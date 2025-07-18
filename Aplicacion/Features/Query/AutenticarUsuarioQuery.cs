using Aplicacion.Dto.Request;
using Aplicacion.Dto.Response;
using Aplicacion.Interfaces;
using Aplicacion.Wrappers;
using AutoMapper;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace Aplicacion.Features.Query
{
    public class AutenticarUsuarioQuery : IRequest<Response<AutenticacionResponse>>
    {
        [Required(ErrorMessage = "El usuario es obligatorio")]
        public string Usuario { get; set; } = null!;
        [Required(ErrorMessage = "El password es obligatorio")]
        public string Password { get; set; } = null!;

        public class AutenticarUsuarioQueryHandler : IRequestHandler<AutenticarUsuarioQuery, Response<AutenticacionResponse>>
        {
            ISeguridadRepository _iSeguridadRepository;
            private readonly IMapper _mapper;

            public AutenticarUsuarioQueryHandler(ISeguridadRepository iSeguridadRepository, IMapper mapper)
            {
                _iSeguridadRepository = iSeguridadRepository;
                _mapper = mapper;
            }

            public async Task<Response<AutenticacionResponse>> Handle(AutenticarUsuarioQuery request, CancellationToken cancellationToken)
            {
                AutenticacionRequest _request = _mapper.Map<AutenticacionRequest>(request);
                AutenticacionResponse result = await _iSeguridadRepository.AutenticarUsuario(_request);
                return new Response<AutenticacionResponse>(result);
            }
        }
    }
}
