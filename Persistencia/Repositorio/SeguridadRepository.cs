using Aplicacion.Dto;
using Aplicacion.Dto.Request;
using Aplicacion.Dto.Response;
using Aplicacion.Exceptions;
using Aplicacion.Interfaces;
using Dominio;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Persistencia.Contexto;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Persistencia.Repositorio
{
    public class SeguridadRepository : ISeguridadRepository
    {
        private readonly DbContextSql _dbContextSql;
        private readonly IOptions<JWTSettings> _jwtSetting;
        public SeguridadRepository(DbContextSql dbContextSql, IOptions<JWTSettings> jwtSetting)
        {
            _dbContextSql = dbContextSql;
            _jwtSetting = jwtSetting;
        }

        /// <summary>
        /// Devuelve el token, enviando un usuario y contraseña
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        /// <exception cref="ApiException"></exception>
        public async Task<AutenticacionResponse> AutenticarUsuario(AutenticacionRequest request)
        {
            try
            {
                #region Validaciones
                //Verifico que el cliente exista
                var usuario = _dbContextSql.Sg001usuario.FirstOrDefault(x => x.Sg001usuario1.Equals(request.Usuario)) ?? throw new ApiException("El usuario no existe");

                //Verifico que el cliente este activo
                if (!usuario.Sg001estado)
                    throw new ApiException("El usuario esta inactivo");
                #endregion

                //Desencriptar contraseña
                string claveDesencriptada = DesencriptarPassword(usuario.Sg001password);
                if(!claveDesencriptada.Equals(request.Password))
                    throw new ApiException("La clave ingresada es incorrecta");

                //Generar token
                JwtSecurityToken jwtSecurityToken = await GenerateJWTToken(request.Usuario);
                AutenticacionResponse response = new()
                {
                    Usuario = request.Usuario,
                    Token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken)
                };

                return response;
            }
            catch (Exception ex)
            {
                throw new ApiException($"Error al autenticarse {ex.Message} {ex.InnerException?.Message}");
            }
        }

        /// <summary>
        /// Permite registrar un nuevo usuario, para poder autenticarse
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        /// <exception cref="ApiException"></exception>
        public async Task<string> RegistrarUsuario(RegistrarUsuarioRequest request)
        {
            //Verifico que el usuario no exista
            if (_dbContextSql.Sg001usuario.Any(x => x.Sg001usuario1.Equals(request.Usuario)))
                throw new ApiException($"El usuario {request.Usuario} ya existe");
            _dbContextSql.Add(new Sg001usuario
            {
                Sg001nombre = request.Nombre,
                Sg001apellido = request.Apellido,
                Sg001usuario1 = request.Usuario,
                Sg001password = EncriptarPassword(request.Password),
                Sg001estado = true,
            });
            await _dbContextSql.SaveChangesAsync();
            return "Cambios guardados exitosamente";
        }

        #region metodos privados

        private async Task<JwtSecurityToken> GenerateJWTToken(string usuario)
        {
            try
            {
                var claims = new[]
                {
                    new Claim(JwtRegisteredClaimNames.Name, usuario),
                };

                var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSetting.Value.Key));
                var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);

                var jwtSecurityToken = new JwtSecurityToken(
                    issuer: _jwtSetting.Value.Issuer,
                    audience: _jwtSetting.Value.Audience,
                    claims: claims,
                    expires: DateTime.Now.AddHours(ConstantDefaults.HorasToken),
                    signingCredentials: signingCredentials
                    );

                return await Task.FromResult(jwtSecurityToken);
            }
            catch (Exception ex)
            {
                throw new ApiException($"Ocurrió un error al momento de generar el Token {ex.Message} {ex.InnerException?.Message}");
            }
        }


        private string EncriptarPassword(string password)
        {
            try
            {
                string result = string.Empty;
                byte[] encryted = Encoding.Unicode.GetBytes(password);
                result = Convert.ToBase64String(encryted);
                return result;
            }
            catch (Exception)
            {
                return string.Empty;
            }
        }

        private string DesencriptarPassword(string password)
        {
            try
            {
                string result = string.Empty;
                byte[] decryted = Convert.FromBase64String(password);
                result = Encoding.Unicode.GetString(decryted);
                return result;
            }
            catch (Exception)
            {
                return string.Empty;
            }
        }
        #endregion
    }
}
