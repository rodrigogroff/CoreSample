using Microsoft.AspNetCore.Mvc;

namespace Master.Controllers
{
    public partial class UserController : MasterController
    {
        [HttpGet("api/v1/user/comments")]
        public ActionResult<string> Comments(int skip, int take)
        {
            SetupNetwork();
            GetAuthentication(ref serviceRequest);

            serviceRequest.AddParameter("skip", skip);
            serviceRequest.AddParameter("take", take);

            return ExecuteRemoteService(serviceClient, serviceRequest);
        }
    }
}
