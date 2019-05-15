using Api.Usuario.Domain;
using Api.Usuario.Json;
using Gateway.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace Api.Usuario.Controllers
{
    [ApiController]
    public class UsuarioController : BaseController
    {
        [HttpPost("api/usuario/autenticar")]
        public ActionResult<string> Post([FromBody] LoginInformation login)
        {
            var serviceLogin = new ServiceLogin();

            var usuarioAutenticado = serviceLogin.Autenticar(login);

            if (!usuarioAutenticado)
                return BadRequest(serviceLogin.Error);

            return Ok(serviceLogin.Auth); 
        }

        [HttpGet("api/usuario/{id}")]
        public ActionResult<string> Get(int id)
        {
            var name = GetName();

            return Ok(new { usuario = ">> " + id  + " => " + name });
        }

        [HttpPost("api/usuario")]
        public ActionResult<string> Post([FromBody] UserInformation login)
        {
            return Ok(new { });
        }
    }
}
