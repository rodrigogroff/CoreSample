using Api.User.Domain;
using Gateway.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace Api.User.Controllers
{
    [ApiController]
    public class UsuarioController : BaseController
    {
        public Features features;

        public UsuarioController(IOptions<Features> feature)
        {
            this.features = feature.Value;
        }

        [HttpPost("api/v1/user/createAccount")]
        public ActionResult<string> createAccount([FromBody] NewUserData newUser)
        {
            if (!features.CreateAccount.Execute)
                return BadRequest(new ServiceError
                {
                    Message = features.CreateAccount.ErrorMessage
                });

            var service = new CreateAccountV1();

            if (!service.CreateAccount(newUser))
                return BadRequest(service.Error);

            return Ok();
        }

        [HttpPost("api/v1/user/authenticate")]
        public ActionResult<string> authenticate([FromBody] LoginInformation login)
        {
            if (!features.Authenticate.Execute)
                return BadRequest(new ServiceError
                {
                    Message = features.CreateAccount.ErrorMessage
                });

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
