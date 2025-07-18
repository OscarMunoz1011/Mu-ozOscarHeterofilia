using Aplicacion.Features.Command;
using Aplicacion.Features.Query;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Presentacion.Controllers
{
    [Authorize]
    public class SeguridadController : BaseApiController
    {
        [HttpPut]
        [Route("AutenticarUsuario")]
        public async Task<IActionResult> AutenticarUsuario(AutenticarUsuarioQuery request)
        {
            return Ok(await Mediator!.Send(request));
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("RegistrarUsuario")]
        public async Task<IActionResult> RegistrarUsuario(RegistrarUsuarioCommand request)
        {
            return Ok(await Mediator!.Send(request));
        }
    }
}
