using Entities.Api.Configuration;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Api.Master.Controllers
{
    public partial class PortalController : MasterController
    {
        [AllowAnonymous]
        [HttpGet("api/v1/portal/categories")]
        public ActionResult<string> PublicPortalCategories(int skip, int take)
        {
            SetupNetwork();
            GetAuthentication(ref serviceRequest);

            serviceRequest.AddParameter("skip", skip);
            serviceRequest.AddParameter("take", take);

            return ExecuteRemoteService(serviceClient, serviceRequest);
        }
    }
}
