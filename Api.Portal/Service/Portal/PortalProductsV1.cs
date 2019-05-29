using Api.Portal.Repository;
using Entities.Api.Configuration;
using Entities.Api;
using System.Data.SqlClient;
using Entities.Api.Portal;

namespace Api.Portal.Service
{
    public class PortalProductsV1
    {
        public ServiceError Error;
        public IPortalRepository repository;        

        public PortalProductsV1(IPortalRepository _repository)
        {
            repository = _repository;
        }

        public PortalProducts Exec(SqlConnection db, long categID, long subcategID, int skip, int take)
        {
            var ret = new PortalProducts();

            foreach (var item in repository.ProductList(db, categID, subcategID, skip, take, ref ret.total))
            {
                ret.list.Add(new ProductSimpleDto
                {
                    Id = item.Id,
                    Name = item.Name,
                    Views = repository.ProductViews(db, item.Id)
                });
            }

            return ret;            
        }
    }
}
