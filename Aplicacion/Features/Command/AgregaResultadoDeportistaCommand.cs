using Aplicacion.Dto.Request;
using Aplicacion.Interfaces;
using Aplicacion.Wrappers;
using AutoMapper;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace Aplicacion.Features.Command
{
    public class AgregaResultadoDeportistaCommand : IRequest<Response<string>>
    {
        [Required(ErrorMessage ="El id de deportista es obligatorio")]
        public int IdDeportista { get; set; }
        [Required(ErrorMessage = "El codigo de modalidad es obligatorio")]
        public string CodigoModalidad { get; set; } = null!;
        [Required(ErrorMessage = "El puntaje es obligatorio")]
        [Range(1,10,ErrorMessage = "El puntaje debe estar entre 1 y 10")]
        public int Puntaje { get; set; }
        public class RegistrarUsuarioCommandHandler : IRequestHandler<RegistrarUsuarioCommand, Response<string>>
        {
            IResultadoRepository _iResultadoRepository;
            private readonly IMapper _mapper;

            public RegistrarUsuarioCommandHandler(IResultadoRepository iResultadoRepository, IMapper mapper)
            {
                _iResultadoRepository = iResultadoRepository;
                _mapper = mapper;
            }

            public async Task<Response<string>> Handle(RegistrarUsuarioCommand request, CancellationToken cancellationToken)
            {
                AgregaResultadoDeportistaRequest _request = _mapper.Map<AgregaResultadoDeportistaRequest>(request);
                string resultado = await _iResultadoRepository.AgregaResultadoDeportista(_request);
                return new Response<string>(null, resultado);
            }
        }
    }
}
