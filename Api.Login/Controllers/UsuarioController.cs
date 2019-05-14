using Api.Usuario.Domain;
using Api.Usuario.Json;
using Gateway.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace Api.Usuario.Controllers
{
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        [HttpPost("api/usuario/autenticar")]
        public ActionResult<JsonResult> Post([FromBody] LoginInformation login)
        {
            var resp = new ServiceLogin().Autenticar(login);

            if (resp == null)
                return BadRequest(new ServiceError
                {
                    Mensagem = "feio!!",
                    Dados = "123",
                    DebugInfo = "..."
                });
            else
                return Ok(resp);
        }

        [HttpGet("api/usuario/{id}")]
        public ActionResult<JsonResult> Get(int id)
        {
            var s = this.Request.Headers[GatewayController.SessionID];

            return Ok(new { usuario = "Nome" + id  + " - " + s });
        }

        [HttpPost("api/usuario")]
        public ActionResult<JsonResult> Post([FromBody] UserInformation login)
        {
            return Ok(new { });
        }
    }
}
