using Microsoft.AspNetCore.Mvc;

namespace Gateway.Controllers
{
    public partial class UserController : GatewayController
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
