using Entities.Api;
using Entities.Api.Configuration;
using Entities.Api.Portal;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Api.Master.Controllers
{
    public partial class PortalController : MasterController
    {
        [AllowAnonymous]
        [HttpPost("api/v1/portal/createAccount")]
        public ActionResult<string> Public_PortalCreateAccount([FromBody] NewUserData obj)
        {
            if (!this.features.CreateAccount.Execute)
                return BadRequest(new ServiceError
                {
                    Message = this.features.CreateAccount.ErrorMessage
                });

            SetupNetwork();
            serviceRequest.AddJsonBody(obj);
            return ExecuteRemoteService(serviceClient, serviceRequest);
        }

        [AllowAnonymous]
        [HttpPost("api/v1/portal/authenticate")]
        public ActionResult<string> Public_PortalAuthenticate([FromBody] LoginInformation obj)
        {
            if (!this.features.Authenticate.Execute)
                return BadRequest(new ServiceError
                {
                    Message = this.features.Authenticate.ErrorMessage
                });

            SetupNetwork();
            serviceRequest.AddJsonBody(obj);

            var resp = ExecuteRemoteService(serviceClient, serviceRequest);

            if (!this.IsOk)
                return resp;

            var auth = JsonConvert.DeserializeObject<AuthenticatedUser>(this.contentServiceResponse);
            var Token = ComposeTokenForSession(auth);

            return Ok(new { Token });
        }

        [AllowAnonymous]
        [HttpGet("api/v1/portal/categories")]
        public ActionResult<string> PortalCategories(int skip, int take)
        {
            SetupNetwork();
            GetAuthentication(ref serviceRequest);

            serviceRequest.AddParameter("skip", skip);
            serviceRequest.AddParameter("take", take);

            return ExecuteRemoteService(serviceClient, serviceRequest);
        }

        [AllowAnonymous]
        [HttpGet("api/v1/portal/subcategories")]
        public ActionResult<string> PortalSubCategories(long categID, int skip, int take)
        {
            SetupNetwork();
            GetAuthentication(ref serviceRequest);

            serviceRequest.AddParameter("categID", categID);
            serviceRequest.AddParameter("skip", skip);
            serviceRequest.AddParameter("take", take);

            return ExecuteRemoteService(serviceClient, serviceRequest);
        }

        [AllowAnonymous]
        [HttpGet("api/v1/portal/product/{id}")]
        public ActionResult<string> PortalProduct(long id)
        {
            SetupNetwork();
            GetAuthentication(ref serviceRequest);

            serviceRequest.AddParameter("id", id);

            return ExecuteRemoteService(serviceClient, serviceRequest);
        }
    }
}
