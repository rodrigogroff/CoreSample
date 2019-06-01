using Entities.Api;
using Entities.Api.Configuration;
using Entities.Api.Portal;
using Master;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Api.Master.Controllers
{
    public partial class PortalController : MasterController
    {
        [AllowAnonymous]
        [HttpGet("api/v1/portal/stats")]
        public ActionResult<string> PortalStats()
        {
            return JsonConvert.SerializeObject (network.GetStats());
        }

        [AllowAnonymous]
        [HttpGet("api/v1/portal/stats_last5")]
        public ActionResult<string> PortalStatsLast5()
        {
            return JsonConvert.SerializeObject(network.GetStats(5));
        }

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
            
            serviceRequest.AddParameter("skip", skip);
            serviceRequest.AddParameter("take", take);
            
            return ExecuteRemoteService(serviceClient, serviceRequest);
        }

        [AllowAnonymous]
        [HttpGet("api/v1/portal/subcategories")]
        public ActionResult<string> PortalSubCategories(long categID, int skip, int take)
        {
            SetupNetwork();
            
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
            
            serviceRequest.AddParameter("id", id);
            serviceRequest.AddParameter("cache", this.features.Cache);

            return ExecuteRemoteService(serviceClient, serviceRequest);
        }

        [AllowAnonymous]
        [HttpGet("api/v1/portal/products")]
        public ActionResult<string> PortalProducts(long categID, long subcategID, int skip, int take)
        {
            if (features.Cache)
                if (CacheGet("_" + categID + "_" + subcategID + "_" + skip + "_" + take + "_", CacheAutomaticRecycle.Normal ))
                    return Ok(contentServiceResponse);

            SetupNetwork();

            serviceRequest.AddParameter("categID", categID);
            serviceRequest.AddParameter("subcategID", subcategID);
            serviceRequest.AddParameter("skip", skip);
            serviceRequest.AddParameter("take", take);

            return ExecuteRemoteService(serviceClient, serviceRequest);
        }
    }
}
