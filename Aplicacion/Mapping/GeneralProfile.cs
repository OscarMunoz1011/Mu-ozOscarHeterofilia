using Aplicacion.Dto.Request;
using Aplicacion.Features.Command;
using Aplicacion.Features.Query;
using AutoMapper;
namespace Aplicacion.Mapping
{
    public class GeneralProfile : Profile
    {
        public GeneralProfile() 
        {
            #region Querys
            CreateMap<AutenticarUsuarioQuery, AutenticacionRequest>();
            #endregion

            #region Commands
            CreateMap<AgregaResultadoDeportistaCommand, AgregaResultadoDeportistaRequest>();
            CreateMap<RegistrarUsuarioCommand, RegistrarUsuarioRequest>();
            #endregion
        }
    }
}
