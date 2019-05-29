using Api.Portal.Repository;
using Entities.Api.Configuration;
using Entities.Api;
using System.Data.SqlClient;

namespace Api.Portal.Service
{
    public class PortalCategoriesV1
    {
        public ServiceError Error;
        public IPortalRepository repository;        

        public PortalCategoriesV1(IPortalRepository _repository)
        {
            repository = _repository;
        }

        public AdminCategories Exec(SqlConnection db, int skip, int take)
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
