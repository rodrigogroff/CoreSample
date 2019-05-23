using Entities.Api;
using Entities.Api.Configuration;
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

        [HttpPost("api/v1/admin/createCategory")]
        public ActionResult<string> AdminCreateCategory([FromBody] NewCategoryData obj)
        {
            if (!this.features.CreateCategory.Execute)
                return BadRequest(new ServiceError
                {
                    Message = this.features.CreateAccount.ErrorMessage
                });

            SetupNetwork();
            serviceRequest.AddJsonBody(obj);
            return ExecuteRemoteService(serviceClient, serviceRequest);
        }

        [HttpPost("api/v1/admin/editCategory")]
        public ActionResult<string> AdminEditCategory([FromBody] NewCategoryData obj)
        {
            SetupNetwork();
            serviceRequest.AddJsonBody(obj);
            return ExecuteRemoteService(serviceClient, serviceRequest);
        }
    }
}
