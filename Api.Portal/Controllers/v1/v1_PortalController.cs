using Api.Portal.Repository;
using Api.Portal.Service;
using Api.Master.Controllers;
using Entities.Api;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System.Data.SqlClient;

namespace Api.Configuration.Controllers
{
    [ApiController]
    public class PortalV1Controller : BaseController
    {
        public PortalRepository repository = new PortalRepository();

        public PortalV1Controller(IConfiguration _configuration)
        {        
            this.configuration = _configuration;
        }

        [HttpGet("api/v1/portal/categories")]
        public ActionResult<string> PortalCategories(int skip, int take)
        {
            try
            {
                using (var db = new SqlConnection(GetDBConnectionString()))
                {
                    var service = new PortalCategoriesV1(repository);
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
