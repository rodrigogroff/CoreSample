using Api.Configuration.Repository;
using Entities.Api.Configuration;
using Entities.Api;
using System.Data.SqlClient;

namespace Api.Configuration.Service
{
    public class AdminSubCategoriesV1
    {
        public ServiceError Error;
        public IAdminRepository repository;        

        public AdminSubCategoriesV1(IAdminRepository _repository)
        {
            repository = _repository;
        }

        public AdminSubCategories Exec(SqlConnection db, AuthenticatedUser au, long categID, int skip, int take)
        {
            var ret = new AdminSubCategories();

            foreach (var item in repository.SubCategoryList(db, categID, skip, take, ref ret.total))
            {
                ret.list.Add(new AdminSubCategory
                {
                    Id = item.Id,
                    Name = item.Name
                });
            }

            return ret;            
        }
    }
}
