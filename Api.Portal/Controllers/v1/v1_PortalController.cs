using Api.Portal.Repository;
using Api.Portal.Service;
using Api.Master.Controllers;
using Entities.Api;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System.Data.SqlClient;
using Entities.Api.Portal;
using Entities.Api.Configuration;

namespace Api.Configuration.Controllers
{
    [ApiController]
    public class PortalV1Controller : BaseController
    {
        public PortalRepository portalRepository = new PortalRepository();
        public UserRepository userRepository = new UserRepository();

        public PortalV1Controller(IConfiguration _configuration)
        {        
            this.configuration = _configuration;
        }

        [HttpPost("api/v1/portal/createAccount")]
        public ActionResult<string> CreateAccount([FromBody] NewUserData newUser)
        {
            try
            {
                using (var db = new SqlConnection(GetDBConnectionString()))
                {
                    var service = new UserCreateAccountV1(userRepository);

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

        [HttpPost("api/v1/portal/authenticate")]
        public ActionResult<string> Authenticate([FromBody] LoginInformation login)
        {
            try
            {
                using (var db = new SqlConnection(GetDBConnectionString()))
                {
                    var service = new UserAuthenticateV1(userRepository);
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

        [HttpGet("api/v1/portal/comments")]
        public ActionResult<string> Comments(int skip, int take)
        {
            try
            {
                using (var db = new SqlConnection(GetDBConnectionString()))
                {
                    var service = new UserCommentsV1(userRepository);
                    var resp = service.Exec(db, GetCurrentAuthenticatedUser(), skip, take);

                    return Ok(JsonConvert.SerializeObject(resp));
                }
            }
            catch (System.Exception ex)
            {
                return BadRequest(new ServiceError { DebugInfo = ex.ToString(), Message = _defaultError });
            }
        }

        [HttpGet("api/v1/portal/categories")]
        public ActionResult<string> PortalCategories(int skip, int take)
        {
            try
            {
                using (var db = new SqlConnection(GetDBConnectionString()))
                {
                    var service = new PortalCategoriesV1(portalRepository);
                    var resp = service.Exec(db, skip, take);

                    return Ok(JsonConvert.SerializeObject(resp));
                }
            }
            catch (System.Exception ex)
            {
                return BadRequest(new ServiceError { DebugInfo = ex.ToString(), Message = _defaultError });
            }
        }

        [HttpGet("api/v1/portal/subcategories")]
        public ActionResult<string> PortalSubCategories(long categID, int skip, int take)
        {
            try
            {
                using (var db = new SqlConnection(GetDBConnectionString()))
                {
                    var service = new PortalSubCategoriesV1(portalRepository);
                    var resp = service.Exec(db, categID, skip, take);

                    return Ok(JsonConvert.SerializeObject(resp));
                }
            }
            catch (System.Exception ex)
            {
                return BadRequest(new ServiceError { DebugInfo = ex.ToString(), Message = _defaultError });
            }
        }

        [HttpGet("api/v1/portal/product_auth/{id}")]
        public ActionResult<string> PortalProductAuth(int id)
        {
            try
            {
                using (var db = new SqlConnection(GetDBConnectionString()))
                {
                    var service = new PortalProductV1(portalRepository);
                    var resp = service.Exec(db, GetCurrentAuthenticatedUser(), id);

                    return Ok(JsonConvert.SerializeObject(resp));
                }
            }
            catch (System.Exception ex)
            {
                return BadRequest(new ServiceError { DebugInfo = ex.ToString(), Message = _defaultError });
            }
        }

        [HttpGet("api/v1/portal/product/{id}")]
        public ActionResult<string> PortalProduct(int id)
        {
            return PortalProductAuth(id);
        }

        [HttpGet("api/v1/portal/products")]
        public ActionResult<string> PortalProducts(long categID, long subcategID, int skip, int take)
        {
            try
            {
                using (var db = new SqlConnection(GetDBConnectionString()))
                {
                    var service = new PortalProductsV1(portalRepository);
                    var resp = service.Exec(db, categID, subcategID, skip, take);

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
