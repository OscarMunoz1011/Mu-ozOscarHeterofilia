using Aplicacion.Dto;
using Aplicacion.Dto.Request;
using Aplicacion.Dto.Response;
using Aplicacion.Exceptions;
using Aplicacion.Interfaces;
using Aplicacion.Wrappers;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Persistencia.Contexto;
using PgpCore;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Text.RegularExpressions;

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
        public async Task<Response<AutenticacionResponse>> AuthenticateAsync(AutenticacionRequest request)
        {
            try
            {
                //Verifico que el cliente exista
                var usuario = _dbContextSql.Sg001usuario.FirstOrDefault(x => x.Sg001usuario1.Equals(request.Usuario)) ?? throw new ApiException("El usuario no existe");

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

                return new Response<AutenticacionResponse>(response, "Usuario autenticado correctamente");
            }
            catch (Exception ex)
            {
                throw new ApiException($"Error al autenticarse {ex.Message} {ex.InnerException?.Message}");
            }
        }

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
                    expires: DateTime.Now.AddHours(24),
                    signingCredentials: signingCredentials
                    );

                return await Task.FromResult(jwtSecurityToken);
            }
            catch (Exception ex)
            {
                throw new ApiException($"Ocurrió un error al momento de generar el Token {ex.Message} {ex.InnerException?.Message}");
            }
        }


        public string EncriptarPassword(string password)
        {
            string resultado = string.Empty;
            try
            {
                var llave = ConstantDefaults.LlavePublica;

                Stream publicKeyStream = new MemoryStream(Convert.FromBase64String(llave));
                EncryptionKeys encryptionKeys = new EncryptionKeys(publicKeyStream);
                PGP pgp = new PGP(encryptionKeys);
                string encryptedContent = pgp.EncryptArmoredString(password);
                encryptedContent = Regex.Replace(encryptedContent, @"\n", "");
                encryptedContent = Regex.Replace(encryptedContent, @"\r", "");
                resultado = encryptedContent.Replace("-----BEGIN PGP MESSAGE-----Version: BCPG C# v1.9.0.0", "").Replace("-----END PGP MESSAGE-----", "");
            }
            catch (Exception ex)
            {
                throw new ApiException($"Error al encriptar la clave {ex.Message} {ex.InnerException?.Message}");
            }
            return resultado;
        }

        public string DesencriptarPassword(string password)
        {
            string resultado = string.Empty;
            try
            {
                
                Stream privateKeyStream = new MemoryStream(Convert.FromBase64String(ConstantDefaults.LlavePublica));
                EncryptionKeys encryptionKeys = new EncryptionKeys(privateKeyStream, password);
                PGP pgp = new PGP(encryptionKeys);
                // Decrypt
                resultado = pgp.DecryptArmoredString(password);
            }
            catch (Exception ex)
            {
                throw new ApiException($"Error al encriptar la clave {ex.Message} {ex.InnerException?.Message}");
            }
            return resultado;
        }
    }
}
