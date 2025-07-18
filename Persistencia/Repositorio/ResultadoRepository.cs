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
        public async Task<List<ResultadosHeterofiliaResponse>> DevuelveResultadosHeterofilia(int? cantidadPaginas, int? cantidadResultados)
        {
            try
            {
                var response = await (from ht4 in _dbcontextSql.Ht004resultado
                                      join ht3 in _dbcontextSql.Ht003modalidad on ht4.Ht003codigo equals ht3.Ht003codigo
                                      join ht2 in _dbcontextSql.Ht002tipoModalidad on ht3.Ht002id equals ht2.Ht002id
                                      join ht1 in _dbcontextSql.Ht001deportista on ht4.Ht001id equals ht1.Ht001id
                                      join cf1 in _dbcontextSql.Cf001pais on ht1.Cf001codigo equals cf1.Cf001codigo
                                      group new { ht1, ht2, ht4 } by new { ht1.Cf001codigo, ht1.Ht001nombre, ht1.Ht001apellido } into gp
                                      let maxArranque = gp.Where(x => x.ht2.Ht002esArranque).Max(y => (int?)y.ht4.Ht004puntaje) ?? 0
                                      let maxEnvion = gp.Where(x => x.ht2.Ht002esEnvion).Max(y => (int?)y.ht4.Ht004puntaje) ?? 0
                                      select new ResultadosHeterofiliaResponse
                                      {
                                          Pais = gp.Key.Cf001codigo,
                                          Nombre = $"{gp.Key.Ht001nombre} {gp.Key.Ht001apellido}",
                                          Arranque = maxArranque,
                                          Envion = maxEnvion,
                                          TotalPeso = maxArranque + maxEnvion
                                      }).ToListAsync();

                //paginacion
                if (cantidadPaginas is null)
                    cantidadPaginas = 1;
                if (cantidadResultados is null)
                    cantidadResultados = 10;

                response = response.Skip((cantidadPaginas.GetValueOrDefault() - 1) * cantidadResultados.GetValueOrDefault())
                           .Take(cantidadResultados.GetValueOrDefault())
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
                int cantidadRegistradas = _dbcontextSql.Ht004resultado.FirstOrDefault(x => x.Ht001id.Equals(request.IdDeportista) && x.Ht003codigo.Equals(request.CodigoModalidad))?.Ht004intento ?? 0;
                if (cantidadRegistradas > 3)
                    throw new ApiException($"Ya no puedes registrar mas intentos, para este deportista");
                _dbcontextSql.Ht004resultado.Add(new Ht004resultado
                {
                    Ht001id = request.IdDeportista,
                    Ht003codigo = request.CodigoModalidad,
                    Ht004intento = cantidadRegistradas++,
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
