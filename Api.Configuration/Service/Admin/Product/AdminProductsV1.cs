using Api.Configuration.Repository;
using Entities.Api.Configuration;
using Entities.Api;
using System.Data.SqlClient;

namespace Api.Configuration.Service
{
    public class AdminProductsV1
    {
        public ServiceError Error;
        public IAdminRepository repository;        

        public AdminProductsV1(IAdminRepository _repository)
        {
            repository = _repository;
        }

        public AdminProducts Exec(SqlConnection db, long categID, long subcategID, int skip, int take)
        {
            var ret = new AdminProducts();

            foreach (var item in repository.ProductList(db, categID, subcategID, skip, take, ref ret.total))
            {
                ret.list.Add(new AdminProductData
                {
                    Id = item.Id,
                    Name = item.Name
                });
            }

            return ret;            
        }
    }
}
