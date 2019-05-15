using Api.Usuario.Domain;
using Api.Usuario.Json;
using Gateway.Controllers;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Api.Usuario.Controllers
{
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        [HttpPost("api/usuario/autenticar")]
        public ActionResult<string> Post([FromBody] LoginInformation login)
        {
            var usuarioAutenticado = new ServiceLogin().Autenticar(login);

            if (!usuarioAutenticado)
                return BadRequest(new ServiceError
                {
                    Mensagem = "feio!!",
                    Dados = "123",
                    DebugInfo = "..."
                });

            return Ok(new LoginAuthentication
            {
                Nome = login.Login + " OK",
                Token = "dsfdsfsdgfdgffd"
            }); 
        }

        [HttpGet("api/usuario/{id}")]
        public ActionResult<string> Get(int id)
        {
            var s = this.Request.Headers[GatewayController.SessionID];

            return Ok(new { usuario = "Nome" + id  + " - " + s });
        }

        [HttpPost("api/usuario")]
        public ActionResult<string> Post([FromBody] UserInformation login)
        {
            return Ok(new { });
        }
    }
}
