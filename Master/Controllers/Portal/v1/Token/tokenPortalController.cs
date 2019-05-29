using Microsoft.AspNetCore.Mvc;

namespace Api.Master.Controllers
{
    public partial class PortalController : MasterController
    {
        [HttpGet("api/v1/portal/comments")]
        public ActionResult<string> Token_UserComments(int skip, int take)
        {
            SetupNetwork();
            GetAuthentication(ref serviceRequest);

            serviceRequest.AddParameter("skip", skip);
            serviceRequest.AddParameter("take", take);

            return ExecuteRemoteService(serviceClient, serviceRequest);
        }
    }
}
