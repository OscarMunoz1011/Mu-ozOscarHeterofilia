using Aplicacion.Dto.Request;
using Aplicacion.Dto.Response;

namespace Aplicacion.Interfaces
{
    public interface IResultadoRepository
    {
        /// <summary>
        /// Devuelve los mejores resultados de arranque y envion por participante
        /// </summary>
        /// <param name="cantidadPaginas"></param>
        /// <param name="cantidadResultados"></param>
        /// <returns></returns>
        /// <exception cref="ApiException"></exception>
        Task<List<ResultadosHeterofiliaResponse>> DevuelveResultadosHeterofilia(int? cantidadPaginas, int? cantidadResultados);

        /// <summary>
        /// Permite ingresar un nuevo valor a un deportista, en la modalidad enviada
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        /// <exception cref="ApiException"></exception>
        Task<string> AgregaResultadoDeportista(AgregaResultadoDeportistaRequest request);

        /// <summary>
        /// Devuelve cantidad de intentos de cada deportista
        /// </summary>
        /// <returns></returns>
        Task<List<IntentoDeportistaResponse>> DevuelveIntentosDeportista();
    }
}
