using Microsoft.AspNetCore.Mvc;

namespace Api.Master.Controllers
{
    public partial class PortalController : MasterController
    {
        [HttpGet("api/v1/portal/comments")]
        public ActionResult<string> Token_UserComments(int skip, int take)
        {
            SetupAuthenticatedNetwork();
            
            serviceRequest.AddParameter("skip", skip);
            serviceRequest.AddParameter("take", take);

            return ExecuteRemoteService(serviceClient, serviceRequest);
        }

        [HttpGet("api/v1/portal/product_auth/{id}")]
        public ActionResult<string> PortalProductAuth(long id)
        {
            SetupAuthenticatedNetwork();
            
            serviceRequest.AddParameter("id", id);

            return ExecuteRemoteService(serviceClient, serviceRequest);
        }
    }
}
