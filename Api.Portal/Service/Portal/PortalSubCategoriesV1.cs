using Api.Portal.Repository;
using Entities.Api.Configuration;
using Entities.Api;
using System.Data.SqlClient;

namespace Api.Portal.Service
{
    public class PortalSubCategoriesV1
    {
        public ServiceError Error;
        public IPortalRepository repository;        

        public PortalSubCategoriesV1(IPortalRepository _repository)
        {
            repository = _repository;
        }

        public AdminSubCategories Exec(SqlConnection db, long categID, int skip, int take)
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
