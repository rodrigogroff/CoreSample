using Api.Configuration.Repository;
using Entities.Api.Configuration;
using Entities.Api;
using System.Data.SqlClient;

namespace Api.Configuration.Service
{
    public class AdminEditSubCategoryV1
    {
        public ServiceError Error;
        public IAdminRepository repository;        

        public AdminEditSubCategoryV1(IAdminRepository _repository)
        {
            repository = _repository;
        }

        public bool Exec(SqlConnection db, NewSubCategoryData obj)
        {
            if (string.IsNullOrEmpty(obj.Name))
            {
                Error = new ServiceError { Message = "Name is empty!" };
                return false;
            }

            if (!repository.SubCategoryExistsId(db, obj.Id))
            {
                Error = new ServiceError { Message = "Id invalid!" };
                return false;
            }

            repository.SubCategoryEdit(db, obj);
            
            return true;
        }
    }
}
