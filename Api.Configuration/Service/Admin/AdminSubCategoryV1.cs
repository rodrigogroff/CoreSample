using Api.Configuration.Repository;
using Entities.Api.Configuration;
using Entities.Api;
using System.Data.SqlClient;

namespace Api.Configuration.Service
{
    public class AdminSubCategoryV1
    {
        public ServiceError Error;
        public IAdminRepository repository;        

        public AdminSubCategoryV1(IAdminRepository _repository)
        {
            repository = _repository;
        }

        public AdminSubCategory Exec(SqlConnection db, AuthenticatedUser au, long id)
        {
            var ret = repository.SubCategoryById(db, id);

            return new AdminSubCategory
            {
                Id = ret.Id,
                Name = ret.Name
            };
        }
    }
}
