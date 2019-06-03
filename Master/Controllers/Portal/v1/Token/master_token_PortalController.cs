using Entities.Api.Portal;
using Microsoft.AspNetCore.Mvc;

namespace Api.Master.Controllers
{
    public partial class PortalController : MasterController
    {
        [HttpGet("api/v1/portal/comments")]
        public ActionResult<string> Token_UserComments(int skip, int take)
        {
            SetupNetwork();
            
            serviceRequest.AddParameter("skip", skip);
            serviceRequest.AddParameter("take", take);

            return ExecuteRemoteService(null, token: true);
        }

        [HttpGet("api/v1/portal/product_auth/{id}")]
        public ActionResult<string> Token_ProductAuth(long id)
        {
            SetupNetwork();
            
            serviceRequest.AddParameter("id", id);

            return ExecuteRemoteService(null, token: true);
        }

        [HttpPost("api/v1/portal/productComment")]
        public ActionResult<string> Token_ProductComment([FromBody] NewProductComment obj)
        {
            return ExecuteRemoteService(obj, token: true);
        }
    }
}
