using Entities.Api;
using Entities.Api.Configuration;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

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

            return ExecuteRemoteService(obj, token: true);
        }

        [HttpPost("api/v1/admin/editCategory")]
        public ActionResult<string> Token_AdminEditCategory([FromBody] NewCategoryData obj)
        {
            return ExecuteRemoteService(obj, token: true);
        }

        [HttpGet("api/v1/admin/categories")]
        public ActionResult<string> Token_AdminCategories(int skip, int take)
        {
            SetupNetwork();

            serviceRequest.AddParameter("skip", skip);
            serviceRequest.AddParameter("take", take);

            return ExecuteRemoteService(null, token: true);
        }

        [HttpGet("api/v1/admin/category/{id}")]
        public ActionResult<string> Token_AdminCategory(long id)
        {
            SetupNetwork();

            serviceRequest.AddParameter("id", id);

            return ExecuteRemoteService(null, token: true);
        }

        [HttpPost("api/v1/admin/createSubCategory")]
        public ActionResult<string> Token_AdminCreateSubCategory([FromBody] NewSubCategoryData obj)
        {
            if (!this.features.CreateSubCategory.Execute)
                return BadRequest(new ServiceError
                {
                    Message = this.features.CreateAccount.ErrorMessage
                });

            return ExecuteRemoteService(obj, token: true);
        }

        [HttpGet("api/v1/admin/subcategories")]
        public ActionResult<string> Token_AdminSubCategories(long categID, int skip, int take)
        {
            SetupNetwork();

            serviceRequest.AddParameter("categID", categID);
            serviceRequest.AddParameter("skip", skip);
            serviceRequest.AddParameter("take", take);

            return ExecuteRemoteService(null, token: true);
        }

        [HttpPost("api/v1/admin/editsubCategory")]
        public ActionResult<string> Token_AdminEditSubCategory([FromBody] NewSubCategoryData obj)
        {
            return ExecuteRemoteService(obj, token: true);
        }

        [HttpGet("api/v1/admin/subcategory/{id}")]
        public ActionResult<string> Token_AdminSubCategory(long id) 
        {
            SetupNetwork();
            serviceRequest.AddParameter("id", id);
            return ExecuteRemoteService(null, token: true);
        }

        [HttpPost("api/v1/admin/createproduct")]
        public ActionResult<string> Token_AdminCreateProduct([FromBody] NewProductData obj)
        {
            if (!this.features.CreateProduct.Execute)
                return BadRequest(new ServiceError
                {
                    Message = this.features.CreateProduct.ErrorMessage
                });

            var tags = new List<string>
            {
                GetCacheMask_Products (obj.ProductCategoryID, obj.ProductSubCategoryID )
            };

            return ExecuteRemoteService(obj, token: true, lstCacheCleanup: tags);
        }

        [HttpPost("api/v1/admin/editProduct")]
        public ActionResult<string> Token_AdminEditProduct([FromBody] NewProductData obj)
        {
            var tags = new List<string>
            {
                "PortalProducts_",
//                GetCacheMask_Products (obj.ProductCategoryID, obj.ProductSubCategoryID )
            };

            return ExecuteRemoteService(obj, token: true, lstCacheCleanup: tags);
        }

        [HttpGet("api/v1/admin/product/{id}")]
        public ActionResult<string> Token_AdminProduct(long id)
        {
            SetupNetwork();
            serviceRequest.AddParameter("id", id);
            return ExecuteRemoteService(null, token: true);
        }

        [HttpGet("api/v1/admin/products")]
        public ActionResult<string> Token_AdminProducts(long categID, long subcategID, int skip, int take)
        {
            SetupNetwork();

            serviceRequest.AddParameter("categID", categID);
            serviceRequest.AddParameter("subcategID", subcategID);
            serviceRequest.AddParameter("skip", skip);
            serviceRequest.AddParameter("take", take);

            return ExecuteRemoteService(null, token: true);
        }
    }
}
