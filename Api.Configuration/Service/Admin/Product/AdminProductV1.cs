using Api.Configuration.Repository;
using Entities.Api.Configuration;
using Entities.Api;
using System.Data.SqlClient;

namespace Api.Configuration.Service
{
    public class AdminProductV1
    {
        public ServiceError Error;
        public IAdminRepository repository;        

        public AdminProductV1(IAdminRepository _repository)
        {
            repository = _repository;
        }

        public AdminProductData Exec(SqlConnection db, long id)
        {
            var prod = repository.ProductById(db, id);
            var categ = repository.CategoryById(db, prod.ProductCategoryID);
            var subcateg = repository.SubCategoryById(db, prod.ProductSubCategoryID);
            var adminCreation = repository.AdminById(db, prod.CreatedByAdminID);
            var adminEdit = repository.AdminById(db, prod.LastEditByAdminID);

            return new AdminProductData
            {
                Id = prod.Id,
                Name = prod.Name,
                AdminCreation = adminCreation.Name,
                AdminEdit = adminEdit.Name,
                DateCreated = prod.DateAdded.ToString("dd/MM/yyyy HH:mm"),
                DateLastEdit = prod.DateEdit.ToString("dd/MM/yyyy HH:mm"),
                CategName = categ.Name,
                SubCategName = subcateg.Name,
                ProductCategoryID = prod.ProductCategoryID,
                ProductSubCategoryID = prod.ProductSubCategoryID,
            };
        }
    }
}
