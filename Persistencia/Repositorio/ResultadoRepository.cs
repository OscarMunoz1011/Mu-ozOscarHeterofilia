using Aplicacion.Dto.Request;
using Aplicacion.Dto.Response;
using Aplicacion.Exceptions;
using Aplicacion.Interfaces;
using Dominio;
using Microsoft.EntityFrameworkCore;
using Persistencia.Contexto;

namespace Persistencia.Repositorio
{
    public class ResultadoRepository : IResultadoRepository
    {
        private readonly DbContextSql _dbcontextSql;
        public ResultadoRepository(DbContextSql dbcontextSql) 
        {
            _dbcontextSql = dbcontextSql;   
        }


        /// <summary>
        /// Devuelve los mejores resultados de arranque y envion por participante
        /// </summary>
        /// <param name="cantidadPaginas"></param>
        /// <param name="cantidadResultados"></param>
        /// <returns></returns>
        /// <exception cref="ApiException"></exception>
        public async Task<List<ResultadosHeterofiliaResponse>> DevuelveResultadosHeterofilia(int? indexPagina, int? cantidadResultados)
        {
            try
            {
                var query = (from ht4 in _dbcontextSql.Ht004resultado
                            join ht3 in _dbcontextSql.Ht003modalidad on ht4.Ht003codigo equals ht3.Ht003codigo
                            join ht2 in _dbcontextSql.Ht002tipoModalidad on ht3.Ht002id equals ht2.Ht002id
                            join ht1 in _dbcontextSql.Ht001deportista on ht4.Ht001id equals ht1.Ht001id
                            where ht1.Ht001estado
                            && ht3.Ht003estado
                            select new
                            {
                                ht1,
                                ht2,
                                ht4
                            }).ToArray();

                //Proceso los datos
                var response = query.GroupBy(x => new { x.ht1.Cf001codigo, x.ht1.Ht001nombre, x.ht1.Ht001apellido }).Select(y =>
                {
                    var maxArranque = y.Where(a => a.ht2.Ht002esArranque).Max(b => (int?)b.ht4.Ht004puntaje) ?? 0;
                    var maxEnvion = y.Where(a => a.ht2.Ht002esEnvion).Max(b => (int?)b.ht4.Ht004puntaje) ?? 0;
                    return new ResultadosHeterofiliaResponse
                    {
                        Pais = y.Key.Cf001codigo,
                        Nombre = $"{y.Key.Ht001nombre} {y.Key.Ht001apellido}",
                        Arranque = maxArranque,
                        Envion = maxEnvion,
                        TotalPeso = maxArranque + maxEnvion
                    };
                }).ToList();
                      

                //Paginacion
                response = response.Skip((indexPagina.GetValueOrDefault(ConstantDefaults.IndexPagina) - 1) * cantidadResultados.GetValueOrDefault(ConstantDefaults.CantidadResultados))
                           .Take(cantidadResultados.GetValueOrDefault(ConstantDefaults.CantidadResultados))
                           .OrderBy(x => x.TotalPeso)
                           .ToList();

                return response;
            }
            catch (Exception ex)
            {
                throw new ApiException($"Error al devolver los datos {ex.Message} {ex.InnerException?.Message}");
            }
        }

        /// <summary>
        /// Permite ingresar un nuevo valor a un deportista, en la modalidad enviada
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        /// <exception cref="ApiException"></exception>
        public async Task<string> AgregaResultadoDeportista(AgregaResultadoDeportistaRequest request)
        {
            try
            {
                #region Validaciones
                //Validar que exista el deportista
                if (!_dbcontextSql.Ht001deportista.Any(x => x.Ht001id.Equals(request.IdDeportista)))
                    throw new ApiException("No existe el deportista con el id enviado");
                
                //Validar que exista la modalidad
                if(!_dbcontextSql.Ht003modalidad.Any(x => x.Ht003codigo.Equals(request.CodigoModalidad)))
                    throw new ApiException("No existe la modalidad con el codigo enviado");

                //Intentos
                int intentoRegistradas = _dbcontextSql.Ht004resultado.Where(x => x.Ht001id.Equals(request.IdDeportista) && x.Ht003codigo.Equals(request.CodigoModalidad)).Max(y => (int?)y.Ht004intento) ?? 0;
                if (intentoRegistradas >= ConstantDefaults.NumeroIntentos)
                    throw new ApiException($"Ya no puedes registrar mas intentos, para este deportista");

                #endregion
                _dbcontextSql.Ht004resultado.Add(new Ht004resultado
                {
                    Ht001id = request.IdDeportista,
                    Ht003codigo = request.CodigoModalidad,
                    Ht004intento = ++intentoRegistradas,
                    Ht004puntaje = request.Puntaje
                });
                await _dbcontextSql.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new ApiException($"Error al registrar un nuevo valor {ex.Message} {ex.InnerException?.Message}");
            }
            return "Cambios guardados exitosamente";
        }
    }
}
