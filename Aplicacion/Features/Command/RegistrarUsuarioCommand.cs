using Aplicacion.Dto.Request;
using Aplicacion.Interfaces;
using Aplicacion.Wrappers;
using AutoMapper;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace Aplicacion.Features.Command
{
    public class RegistrarUsuarioCommand : IRequest<Response<string>>
    {
        [Required(ErrorMessage = "El nombre es obligatorio")]
        public string Nombre { get; set; } = null!;
        [Required(ErrorMessage = "El apellido es obligatorio")]
        public string Apellido { get; set; } = null!;
        [Required(ErrorMessage = "El usuario es obligatorio")]
        public string Usuario { get; set; } = null!;
        [Required(ErrorMessage = "El password es obligatorio")]
        public string Password { get; set; } = null!;
        public class RegistrarUsuarioCommandHandler : IRequestHandler<RegistrarUsuarioCommand, Response<string>>
        {
            ISeguridadRepository _iSeguridadRepository;
            private readonly IMapper _mapper;

            public RegistrarUsuarioCommandHandler(ISeguridadRepository iSeguridadRepository, IMapper mapper)
            {
                _iSeguridadRepository = iSeguridadRepository;
                _mapper = mapper;
            }

            public async Task<Response<string>> Handle(RegistrarUsuarioCommand request, CancellationToken cancellationToken)
            {
                RegistrarUsuarioRequest _request = _mapper.Map<RegistrarUsuarioRequest>(request);
                string resultado = await _iSeguridadRepository.RegistrarUsuario(_request);
                return new Response<string>(null, resultado);
            }
        }

    }
}
