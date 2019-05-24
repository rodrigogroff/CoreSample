using Entities.Api;
using Entities.Api.Configuration;
using Microsoft.AspNetCore.Mvc;

namespace Api.Master.Controllers
{
    public partial class ConfigurationController : MasterController
    {
        [HttpPost("api/v1/admin/createCategory")]
        public ActionResult<string> Token_AdminCreateCategory([FromBody] NewCategoryData obj)
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
        public ActionResult<string> Token_AdminEditCategory([FromBody] NewCategoryData obj)
        {
            SetupNetwork();
            serviceRequest.AddJsonBody(obj);
            return ExecuteRemoteService(serviceClient, serviceRequest);
        }

        [HttpGet("api/v1/admin/categories")]
        public ActionResult<string> Token_AdminCategories(int skip, int take)
        {
            SetupNetwork();
            GetAuthentication(ref serviceRequest);

            serviceRequest.AddParameter("skip", skip);
            serviceRequest.AddParameter("take", take);

            return ExecuteRemoteService(serviceClient, serviceRequest);
        }

        [HttpGet("api/v1/admin/category/{id}")]
        public ActionResult<string> Token_AdminCategory(long id)
        {
            SetupNetwork();
            GetAuthentication(ref serviceRequest);

            serviceRequest.AddParameter("id", id);

            return ExecuteRemoteService(serviceClient, serviceRequest);
        }

        [HttpPost("api/v1/admin/createSubCategory")]
        public ActionResult<string> Token_AdminCreateSubCategory([FromBody] NewSubCategoryData obj)
        {
            SetupNetwork();
            serviceRequest.AddJsonBody(obj);
            return ExecuteRemoteService(serviceClient, serviceRequest);
        }
    }
}
