using Aplicacion.Dto.Response;
using Aplicacion.Interfaces;
using Aplicacion.Wrappers;
using AutoMapper;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace Aplicacion.Features.Query
{
    public class DevuelveResultadosHeterofiliaQuery : IRequest<Response<List<ResultadosHeterofiliaResponse>>>
    {
        [Range(1, int.MaxValue, ErrorMessage = "El número de página debe ser mayor a cero")]
        public int? IndexPagina { get; set; }
        [Range(1, int.MaxValue, ErrorMessage = "El número de datos por pagina debe ser mayor a cero")]
        public int? CantidadResultados { get; set; }

        public class DevuelveResultadosHeterofiliaQueryHandler : IRequestHandler<DevuelveResultadosHeterofiliaQuery, Response<List<ResultadosHeterofiliaResponse>>>
        {
            IResultadoRepository _iResultadoRepository;
            private readonly IMapper _mapper;

            public DevuelveResultadosHeterofiliaQueryHandler(IResultadoRepository iResultadoRepository, IMapper mapper)
            {
                _iResultadoRepository = iResultadoRepository;
                _mapper = mapper;
            }

            public async Task<Response<List<ResultadosHeterofiliaResponse>>> Handle(DevuelveResultadosHeterofiliaQuery request, CancellationToken cancellationToken)
            {
                List<ResultadosHeterofiliaResponse> result = await _iResultadoRepository.DevuelveResultadosHeterofilia(request.IndexPagina, request.CantidadResultados);
                return new Response<List<ResultadosHeterofiliaResponse>>(result);
            }
        }
    }
}
