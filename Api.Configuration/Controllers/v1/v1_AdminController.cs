using Api.Configuration.Repository;
using Api.Configuration.Service;
using Api.Master.Controllers;
using Entities.Api;
using Entities.Api.Configuration;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;

namespace Api.Configuration.Controllers
{
    [ApiController]
    public class AdminV1Controller : BaseController
    {
        public AdminRepository repository = new AdminRepository();

        public AdminV1Controller(IConfiguration _configuration)
        {        
            this.configuration = _configuration;
        }

        [HttpPost("api/v1/admin/createAccount")]
        public ActionResult<string> CreateAccount([FromBody] NewUserData newUser)
        {
            try
            {
                using (var db = new SqlConnection(GetDBConnectionString()))
                {
                    var service = new AdminCreateAccountV1(repository);

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

        [HttpPost("api/v1/admin/authenticate")]
        public ActionResult<string> Authenticate([FromBody] LoginInformation login)
        {
            try
            {
                using (var db = new SqlConnection(GetDBConnectionString()))
                {
                    var service = new AdminAuthenticateV1(repository);
                    var ua = new AuthenticatedUser();

                    if (!service.Authenticate(db, login, ref ua))
                        return BadRequest(service.Error);

                    return Ok(ua);
                }
            }
            catch (System.Exception ex)
            {
                return BadRequest(new ServiceError { DebugInfo = ex.ToString(), Message = _defaultError });
            }
        }
    }
}
