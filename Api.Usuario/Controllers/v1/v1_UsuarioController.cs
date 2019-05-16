using Api.Usuario.Domain;
using Api.Usuario.Json;
using Gateway.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace Api.Usuario.Controllers
{
    [ApiController]
    public class UsuarioController : BaseController
    {
        [HttpPost("api/v1/usuario/autenticar")]
        public ActionResult<string> Post([FromBody] LoginInformation login)
        {
            var serviceLogin = new ServiceLoginV1();

            var usuarioAutenticado = serviceLogin.Autenticar(login);

            if (!usuarioAutenticado)
                return BadRequest(serviceLogin.Error);

            return Ok(serviceLogin.Auth); 
        }

        [HttpGet("api/v1/usuario/{id}")]
        public ActionResult<string> Get(int id)
        {
            var ua = ObtemUsuarioAutenticado();

            return Ok(ua);
        }

        [HttpPost("api/v1/usuario")]
        public ActionResult<string> Post([FromBody] UserInformation login)
        {
            return Ok(new { });
        }
    }
}
