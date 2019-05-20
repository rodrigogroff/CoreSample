using Api.User.Domain;
using Gateway.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using System.Data.SqlClient;

namespace Api.User.Controllers
{
    [ApiController]
    public class UsuarioController : BaseController
    {
        public Features features;
        
        public UsuarioController(IOptions<Features> feature, IConfiguration configuration)
        {
            this.features = feature.Value;
            this._config = configuration;
        }

        [HttpPost("api/v1/user/createAccount")]
        public ActionResult<string> createAccount([FromBody] NewUserData newUser)
        {
            if (!features.CreateAccount.Execute)
                return BadRequest(new ServiceError
                {
                    Message = features.CreateAccount.ErrorMessage
                });

            try
            {
                using (SqlConnection db = new SqlConnection(GetDBConnectionString()))
                {
                    var service = new CreateAccountV1();

                    if (!service.CreateAccount(newUser, db))
                        return BadRequest(service.Error);

                    return Ok();
                }
            }
            catch (System.Exception ex)
            {
                return BadRequest(new ServiceError
                {
                    DebugInfo = ex.ToString(),
                    Message = "Ops. Something happened.",                    
                });
            }
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
    }
}
