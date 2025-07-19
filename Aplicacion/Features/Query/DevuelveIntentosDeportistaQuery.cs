using Aplicacion.Dto.Response;
using Aplicacion.Interfaces;
using Aplicacion.Wrappers;
using AutoMapper;
using MediatR;

namespace Aplicacion.Features.Query
{
    public class DevuelveIntentosDeportistaQuery : IRequest<Response<List<IntentoDeportistaResponse>>>
    {
        public class DevuelveIntentosDeportistaQueryHandler : IRequestHandler<DevuelveIntentosDeportistaQuery, Response<List<IntentoDeportistaResponse>>>
        {
            IResultadoRepository _iResultadoRepository;
            private readonly IMapper _mapper;

            public DevuelveIntentosDeportistaQueryHandler(IResultadoRepository iResultadoRepository, IMapper mapper)
            {
                _iResultadoRepository = iResultadoRepository;
                _mapper = mapper;
            }

            public async Task<Response<List<IntentoDeportistaResponse>>> Handle(DevuelveIntentosDeportistaQuery request, CancellationToken cancellationToken)
            {
                List<IntentoDeportistaResponse> result = await _iResultadoRepository.DevuelveIntentosDeportista();
                return new Response<List<IntentoDeportistaResponse>>(result);
            }
        }
    }
}
