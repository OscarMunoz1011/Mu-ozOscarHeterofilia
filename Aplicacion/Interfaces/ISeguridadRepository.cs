using Aplicacion.Dto.Request;
using Aplicacion.Dto.Response;
using Aplicacion.Wrappers;

namespace Aplicacion.Interfaces
{
    public interface ISeguridadRepository
    {
        /// <summary>
        /// Devuelve el token, enviando un usuario y contraseña
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        /// <exception cref="ApiException"></exception>
        Task<AutenticacionResponse> AutenticarUsuario(AutenticacionRequest request);

        /// <summary>
        /// Permite registrar un nuevo usuario, para poder autenticarse
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        /// <exception cref="ApiException"></exception>
        Task<string> RegistrarUsuario(RegistrarUsuarioRequest request);
    }
}
