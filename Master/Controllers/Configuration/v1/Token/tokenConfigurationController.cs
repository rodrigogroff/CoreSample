using Microsoft.AspNetCore.Mvc;

namespace Api.Master.Controllers
{
    public partial class ConfigurationController : MasterController
    {
        [HttpGet("api/v1/user/comments")]
        public ActionResult<string> UserComments(int skip, int take)
        {
            SetupNetwork();
            GetAuthentication(ref serviceRequest);

            serviceRequest.AddParameter("skip", skip);
            serviceRequest.AddParameter("take", take);

            return ExecuteRemoteService(serviceClient, serviceRequest);
        }
    }
}
