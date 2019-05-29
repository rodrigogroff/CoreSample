using Api.Portal.Repository;
using Entities.Api.Configuration;
using Entities.Api;
using System.Data.SqlClient;
using Entities.Api.Portal;

namespace Api.Portal.Service
{
    public class PortalProductV1
    {
        public ServiceError Error;
        public IPortalRepository repository;        

        public PortalProductV1(IPortalRepository _repository)
        {
            repository = _repository;
        }

        public ProductDto Exec(SqlConnection db, AuthenticatedUser au, long Id)
        {
            var ret = repository.ProductById(db, Id);

            if (ret == null)
            {
                Error = new ServiceError { Message = "Invalid Product ID" };
                return null;
            }

            var retCateg = repository.CategoryById(db, ret.ProductCategoryID);
            var retSubCateg = repository.SubCategoryById(db, ret.ProductSubCategoryID);

            if (au != null)
            {
                repository.ProductAddView(db, au.Id, Id);
            }

            return new ProductDto
            {
                Id = ret.Id,
                Name = ret.Name,
                Views = repository.ProductViews(db, Id),
                Category = retCateg.Name,
                SubCategory = retSubCateg.Name
            };
        }
    }
}
