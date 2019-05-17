using Api.Usuario.Domain;
using Api.Usuario.Json;
using Gateway.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace Api.Usuario.Controllers
{
    [ApiController]
    public class UsuarioController : BaseController
    {
        [HttpPost("api/v1/user/createAccount")]
        public ActionResult<string> createAccount([FromBody] NewUserData newUser)
        {
            var service = new CreateAccountV1();

            if (!service.CreateAccount(newUser))
                return BadRequest(service.Error);

            return Ok();
        }

        [HttpPost("api/v1/user/authenticate")]
        public ActionResult<string> authenticate([FromBody] LoginInformation login)
        {
            var service = new AuthenticateV1();

            if (!service.authenticate(login))
                return BadRequest(service.Error);

            return Ok(service.loggedUser); 
        }

        [HttpGet("api/v1/user/{id}")]
        public ActionResult<string> Get(int id)
        {
            var ua = GetCurrentAuthenticatedUser();

            return Ok(ua);
        }

        /*
        [HttpPost("api/v1/usuario/atualizarconta")]
        public ActionResult<string> AtualizarDados([FromBody] UserInformation login)
        {
            return Ok(new { });
        }
        */
    }
}
