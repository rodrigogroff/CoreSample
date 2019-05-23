using Api.Configuration.Repository;
using Entities.Api.Configuration;
using Entities.Api;
using System.Data.SqlClient;

namespace Api.Configuration.Service
{
    public class AdminCreateCategoryV1
    {
        public ServiceError Error;
        public IAdminRepository repository;        

        public AdminCreateCategoryV1(IAdminRepository _repository)
        {
            repository = _repository;
        }

        public bool Exec(SqlConnection db, NewCategoryData obj)
        {
            if (string.IsNullOrEmpty(obj.Name))
            {
                Error = new ServiceError { Message = "Name is empty!" };
                return false;
            }
            
            if (repository.CategoryExists (db, obj.Name))
            {
                Error = new ServiceError { Message = "Name already exists!" };
                return false;
            }

            repository.CategoryAdd(db, obj);
            
            return true;
        }
    }
}
