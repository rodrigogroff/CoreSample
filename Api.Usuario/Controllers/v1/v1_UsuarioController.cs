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
        public ActionResult<string> Autenticar([FromBody] LoginInformation login)
        {
            var service = new ServiceLoginV1();

            if (!service.Autenticar(login))
                return BadRequest(service.Error);

            return Ok(service.UsuarioLogado); 
        }

        [HttpPost("api/v1/usuario/criarconta")]
        public ActionResult<string> CriarConta([FromBody] DadosNovaConta novoUsuario)
        {
            var service = new CriarContaV1();

            if (!service.CriarConta(novoUsuario))
                return BadRequest(service.Error);

            return Ok();
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
