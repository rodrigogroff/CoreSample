using Api.Configuration.Repository;
using Entities.Api.Configuration;
using Entities.Api;
using System.Data.SqlClient;

namespace Api.Configuration.Service
{
    public class AdminCategoriesV1
    {
        public ServiceError Error;
        public IAdminRepository repository;        

        public AdminCategoriesV1(IAdminRepository _repository)
        {
            repository = _repository;
        }

        public AdminCategories Exec(SqlConnection db, AuthenticatedUser au, int skip, int take)
        {
            var ret = new AdminCategories();

            foreach (var item in repository.CategoryList(db, skip, take, ref ret.total))
            {
                ret.list.Add(new AdminCategory
                {
                    Id = item.Id,
                    Name = item.Name
                });
            }

            return ret;            
        }
    }
}
