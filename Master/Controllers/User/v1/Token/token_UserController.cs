using Microsoft.AspNetCore.Mvc;

namespace Master.Controllers
{
    public partial class UserController : MasterController
    {
        [HttpGet("api/v1/user/{id}")]
        public ActionResult<string> Get(int id)
        {
            SetupNetwork();
            GetAuthentication(ref serviceRequest);
            return ExecuteRemoteService(serviceClient, serviceRequest);
        }
    }
}
