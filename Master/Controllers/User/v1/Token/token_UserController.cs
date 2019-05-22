using Microsoft.AspNetCore.Mvc;

namespace Master.Controllers
{
    public partial class UserController : MasterController
    {
        [HttpGet("api/v1/user/comments")]
        public ActionResult<string> Comments()
        {
            SetupNetwork();
            GetAuthentication(ref serviceRequest);
            return ExecuteRemoteService(serviceClient, serviceRequest);
        }
    }
}
