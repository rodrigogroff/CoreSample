using Api.Configuration.Repository;
using Entities.Api.Configuration;
using Entities.Api;
using System.Data.SqlClient;
using Entities.Database;
using System;

namespace Api.Configuration.Service
{
    public class AdminCreateProductV1
    {
        public ServiceError Error;
        public IAdminRepository repository;

        public long IdCreated = 0;

        public AdminCreateProductV1(IAdminRepository _repository)
        {
            repository = _repository;
        }

        public bool Exec(SqlConnection db, AuthenticatedUser ua, NewProductData obj)
        {
            if (string.IsNullOrEmpty(obj.Name))
            {
                Error = new ServiceError { Message = "Name is empty" };
                return false;
            }

            if (repository.CategoryById(db, obj.ProductCategoryID) == null)
            {
                Error = new ServiceError { Message = "Invalid product category" };
                return false;
            }

            if (repository.SubCategoryById(db, obj.ProductSubCategoryID) == null)
            {
                Error = new ServiceError { Message = "Invalid product subcategory" };
                return false;
            }

            if (repository.ProductExists (db, obj.ProductCategoryID, obj.ProductSubCategoryID, obj.Name))
            {
                Error = new ServiceError { Message = "Product already exists" };
                return false;
            }

            IdCreated = repository.ProductAdd(db, new Product
            {
                CreatedByAdminID = ua.Id,
                LastEditByAdminID = ua.Id,
                DateAdded = DateTime.Now,
                DateEdit = DateTime.Now,
                Name = obj.Name,
                ProductCategoryID = obj.ProductCategoryID,
                ProductSubCategoryID = obj.ProductSubCategoryID
            });
            
            return true;
        }
    }
}
