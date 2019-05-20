using Api.User.Repository;
using Api.User.Service;
using Gateway.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
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
            #region - code - 

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

            #endregion
        }

        [HttpPost("api/v1/user/authenticate")]
        public ActionResult<string> Authenticate([FromBody] LoginInformation login)
        {
            #region - code - 
            try
            {
                var service = new AuthenticateV1(repository);

                if (!service.authenticate(login))
                    return BadRequest(service.Error);

                return Ok(service.loggedUser);
            }
            catch (System.Exception ex)
            {
                return BadRequest(new ServiceError { DebugInfo = ex.ToString(), Message = _defaultError });
            }
            #endregion
        }

        [HttpGet("api/v1/user/{id}")]
        public ActionResult<string> Get(int id)
        {
            #region - code - 
            try
            {
                var ua = GetCurrentAuthenticatedUser();

                return Ok(ua);
            }
            catch (System.Exception ex)
            {
                return BadRequest(new ServiceError { DebugInfo = ex.ToString(), Message = _defaultError });
            }
            #endregion
        }
    }
}
