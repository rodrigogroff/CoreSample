using Api.Configuration.Repository;
using Api.Configuration.Service;
using Api.Master.Controllers;
using Entities.Api;
using Entities.Api.Configuration;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
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

                    if (!service.Exec(db, newUser))
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

                    if (!service.Exec(db, login, ref ua))
                        return BadRequest(service.Error);

                    return Ok(ua);
                }
            }
            catch (System.Exception ex)
            {
                return BadRequest(new ServiceError { DebugInfo = ex.ToString(), Message = _defaultError });
            }
        }

        [HttpPost("api/v1/admin/createCategory")]
        public ActionResult<string> CreateCategory([FromBody] NewCategoryData obj)
        {
            try
            {
                using (var db = new SqlConnection(GetDBConnectionString()))
                {
                    var service = new AdminCreateCategoryV1(repository);

                    if (!service.Exec(db, obj))
                        return BadRequest(service.Error);

                    return Ok();
                }
            }
            catch (System.Exception ex)
            {
                return BadRequest(new ServiceError { DebugInfo = ex.ToString(), Message = _defaultError });
            }
        }

        [HttpPost("api/v1/admin/editCategory")]
        public ActionResult<string> EditCategory([FromBody] NewCategoryData obj)
        {
            try
            {
                using (var db = new SqlConnection(GetDBConnectionString()))
                {
                    var service = new AdminEditCategoryV1(repository);

                    if (!service.Exec(db, obj))
                        return BadRequest(service.Error);

                    return Ok();
                }
            }
            catch (System.Exception ex)
            {
                return BadRequest(new ServiceError { DebugInfo = ex.ToString(), Message = _defaultError });
            }
        }

        [HttpGet("api/v1/admin/categories")]
        public ActionResult<string> AdminCategories(int skip, int take)
        {
            try
            {
                using (var db = new SqlConnection(GetDBConnectionString()))
                {
                    var service = new AdminCategoriesV1(repository);
                    var resp = service.Exec(db, GetCurrentAuthenticatedUser(), skip, take);

                    return Ok(JsonConvert.SerializeObject(resp));
                }
            }
            catch (System.Exception ex)
            {
                return BadRequest(new ServiceError { DebugInfo = ex.ToString(), Message = _defaultError });
            }
        }
    }
}
