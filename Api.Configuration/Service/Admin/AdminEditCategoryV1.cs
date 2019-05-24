using Api.Configuration.Repository;
using Entities.Api.Configuration;
using Entities.Api;
using System.Data.SqlClient;

namespace Api.Configuration.Service
{
    public class AdminEditCategoryV1
    {
        public ServiceError Error;
        public IAdminRepository repository;        

        public AdminEditCategoryV1(IAdminRepository _repository)
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

            if (!repository.CategoryExistsId(db, obj.Id))
            {
                Error = new ServiceError { Message = "Id invalid!" };
                return false;
            }

            repository.CategoryEdit(db, obj);
            
            return true;
        }
    }
}
