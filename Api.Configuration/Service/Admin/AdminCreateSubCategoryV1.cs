using Api.Configuration.Repository;
using Entities.Api.Configuration;
using Entities.Api;
using System.Data.SqlClient;

namespace Api.Configuration.Service
{
    public class AdminCreateSubCategoryV1
    {
        public ServiceError Error;
        public IAdminRepository repository;

        public long IdCreated = 0;

        public AdminCreateSubCategoryV1(IAdminRepository _repository)
        {
            repository = _repository;
        }

        public bool Exec(SqlConnection db, NewSubCategoryData obj)
        {
            if (obj.ProductCategoryID == 0)
            {
                Error = new ServiceError { Message = "CategoryID is empty" };
                return false;
            }

            if (!repository.CategoryExistsId(db, obj.ProductCategoryID))
            {
                Error = new ServiceError { Message = "CategoryID is invalid" };
                return false;
            }

            if (string.IsNullOrEmpty(obj.Name))
            {
                Error = new ServiceError { Message = "Name is empty" };
                return false;
            }
            
            if (repository.SubCategoryExists (db, obj.ProductCategoryID, obj.Name))
            {
                Error = new ServiceError { Message = "Name already exists" };
                return false;
            }

            IdCreated = repository.SubCategoryAdd(db, obj);
            
            return true;
        }
    }
}
