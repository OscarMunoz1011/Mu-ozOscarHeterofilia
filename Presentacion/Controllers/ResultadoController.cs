using Aplicacion.Features.Command;
using Aplicacion.Features.Query;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Presentacion.Controllers
{
    [Authorize]
    public class ResultadoController : BaseApiController
    {
        [HttpGet]
        [Route("DevuelveResultadosHeterofiliaQuery")]
        public async Task<IActionResult> DevuelveResultadosHeterofilia([FromQuery] DevuelveResultadosHeterofiliaQuery request)
        {
            return Ok(await Mediator!.Send(request));
        }

        [HttpPost]
        [Route("AgregaResultadoDeportista")]
        public async Task<IActionResult> AgregaResultadoDeportista(AgregaResultadoDeportistaCommand request)
        {
            return Ok(await Mediator!.Send(request));
        }

        [HttpGet]
        [Route("DevuelveIntentosDeportista")]
        public async Task<IActionResult> DevuelveIntentosDeportista()
        {
            return Ok(await Mediator.Send(new DevuelveIntentosDeportistaQuery()));
        }
    }
}


