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

            SetupAuthenticatedNetwork();
            serviceRequest.AddJsonBody(obj);
            return ExecuteRemoteService(serviceClient, serviceRequest);
        }

        [HttpPost("api/v1/admin/editCategory")]
        public ActionResult<string> Token_AdminEditCategory([FromBody] NewCategoryData obj)
        {
            SetupAuthenticatedNetwork();
            serviceRequest.AddJsonBody(obj);
            return ExecuteRemoteService(serviceClient, serviceRequest);
        }

        [HttpGet("api/v1/admin/categories")]
        public ActionResult<string> Token_AdminCategories(int skip, int take)
        {
            SetupAuthenticatedNetwork();

            serviceRequest.AddParameter("skip", skip);
            serviceRequest.AddParameter("take", take);

            return ExecuteRemoteService(serviceClient, serviceRequest);
        }

        [HttpGet("api/v1/admin/category/{id}")]
        public ActionResult<string> Token_AdminCategory(long id)
        {
            SetupAuthenticatedNetwork();
            serviceRequest.AddParameter("id", id);
            return ExecuteRemoteService(serviceClient, serviceRequest);
        }

        [HttpPost("api/v1/admin/createSubCategory")]
        public ActionResult<string> Token_AdminCreateSubCategory([FromBody] NewSubCategoryData obj)
        {
            if (!this.features.CreateSubCategory.Execute)
                return BadRequest(new ServiceError
                {
                    Message = this.features.CreateAccount.ErrorMessage
                });

            SetupAuthenticatedNetwork();
            serviceRequest.AddJsonBody(obj);
            return ExecuteRemoteService(serviceClient, serviceRequest);
        }

        [HttpGet("api/v1/admin/subcategories")]
        public ActionResult<string> Token_AdminSubCategories(long categID, int skip, int take)
        {
            SetupAuthenticatedNetwork();

            serviceRequest.AddParameter("categID", categID);
            serviceRequest.AddParameter("skip", skip);
            serviceRequest.AddParameter("take", take);

            return ExecuteRemoteService(serviceClient, serviceRequest);
        }

        [HttpPost("api/v1/admin/editsubCategory")]
        public ActionResult<string> Token_AdminEditSubCategory([FromBody] NewSubCategoryData obj)
        {
            SetupAuthenticatedNetwork();
            serviceRequest.AddJsonBody(obj);
            return ExecuteRemoteService(serviceClient, serviceRequest);
        }

        [HttpGet("api/v1/admin/subcategory/{id}")]
        public ActionResult<string> Token_AdminSubCategory(long id) 
        {
            SetupAuthenticatedNetwork();
            serviceRequest.AddParameter("id", id);
            return ExecuteRemoteService(serviceClient, serviceRequest);
        }

        [HttpPost("api/v1/admin/createproduct")]
        public ActionResult<string> Token_AdminCreateProduct([FromBody] NewProductData obj)
        {
            if (!this.features.CreateProduct.Execute)
                return BadRequest(new ServiceError
                {
                    Message = this.features.CreateProduct.ErrorMessage
                });

            SetupAuthenticatedNetwork();
            serviceRequest.AddJsonBody(obj);
            return ExecuteRemoteService(serviceClient, serviceRequest);
        }

        [HttpPost("api/v1/admin/editProduct")]
        public ActionResult<string> Token_AdminEditProduct([FromBody] NewProductData obj)
        {
            SetupAuthenticatedNetwork();
            serviceRequest.AddJsonBody(obj);
            return ExecuteRemoteService(serviceClient, serviceRequest);
        }

        [HttpGet("api/v1/admin/product/{id}")]
        public ActionResult<string> Token_AdminProduct(long id)
        {
            SetupAuthenticatedNetwork();
            serviceRequest.AddParameter("id", id);
            return ExecuteRemoteService(serviceClient, serviceRequest);
        }
    }
}
