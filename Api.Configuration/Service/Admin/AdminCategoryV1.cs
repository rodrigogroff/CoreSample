using Api.Configuration.Repository;
using Entities.Api.Configuration;
using Entities.Api;
using System.Data.SqlClient;

namespace Api.Configuration.Service
{
    public class AdminCategoryV1
    {
        public ServiceError Error;
        public IAdminRepository repository;        

        public AdminCategoryV1(IAdminRepository _repository)
        {
            repository = _repository;
        }

        public AdminCategory Exec(SqlConnection db, AuthenticatedUser au, long id)
        {
            var ret = repository.CategoryById(db, id);

            return new AdminCategory
            {
                Id = ret.Id,
                Name = ret.Name
            };
        }
    }
}
