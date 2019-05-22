using Api.User.Repository;
using Api.User.Service;
using Master.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System.Data.SqlClient;

namespace Api.User.Controllers
{
    [ApiController]
    public class UserController : BaseController
    {
        public UserRepository repository = new UserRepository();

        public UserController(IConfiguration _configuration)
        {        
            this.configuration = _configuration;
        }

        [HttpPost("api/v1/user/createAccount")]
        public ActionResult<string> CreateAccount([FromBody] NewUserData newUser)
        {
            try
            {
                using (SqlConnection db = new SqlConnection(GetDBConnectionString()))
                {
                    var service = new CreateAccountV1(repository);

                    if (!service.CreateAccount(db, newUser))
                        return BadRequest(service.Error);

                    return Ok();
                }
            }
            catch (System.Exception ex)
            {
                return BadRequest(new ServiceError { DebugInfo = ex.ToString(), Message = _defaultError });
            }
        }

        [HttpPost("api/v1/user/authenticate")]
        public ActionResult<string> Authenticate([FromBody] LoginInformation login)
        {
            try
            {
                using (SqlConnection db = new SqlConnection(GetDBConnectionString()))
                {
                    var service = new AuthenticateV1(repository);

                    if (!service.authenticate(db, login))
                        return BadRequest(service.Error);

                    return Ok(service.loggedUser);
                }
            }
            catch (System.Exception ex)
            {
                return BadRequest(new ServiceError { DebugInfo = ex.ToString(), Message = _defaultError });
            }
        }

        [HttpGet("api/v1/user/comments")]
        public ActionResult<string> Comments()
        {
            try
            {
                var service = new UserActionsV1(repository);
                var au = GetCurrentAuthenticatedUser();
                var resp = service.Comments(au);
                return Ok(JsonConvert.SerializeObject(resp));
            }
            catch (System.Exception ex)
            {
                return BadRequest(new ServiceError { DebugInfo = ex.ToString(), Message = _defaultError });
            }
        }
    }
}
