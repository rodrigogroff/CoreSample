using Api.Portal.Repository;
using Entities.Database;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace UnitTesting
{
    public class mockPortalRepository : IPortalRepository
    {
        public ProductCategory CategoryById(SqlConnection db, long Id)
        {
            return new ProductCategory
            {
                Id = Id,
                Name = "test"
            };
        }

        public List<ProductCategory> CategoryList(SqlConnection db, int skip, int take, ref int total)
        {
            total = 1;
            return new List<ProductCategory>
            {
                new ProductCategory
                {
                    Id = 1,
                    Name = "teste"
                }
            };
        }

        public long ProductAddView(SqlConnection db, long userId, long productId)
        {
            return 1;
        }

        public Product ProductById(SqlConnection db, long Id)
        {
            return new Product
            {
                Id = Id,
                Name = "test",
                ProductCategoryID = 1,
                ProductSubCategoryID = 1
            };
        }

        public List<Product> ProductList(SqlConnection db, long categID, long subcategID, int skip, int take, ref int total)
        {
            return new List<Product>
            {
                new Product
                {
                    Id = 1,
                    Name = "test"
                }
            };
        }

        public long ProductViews(SqlConnection db, long productId)
        {
            return 10;
        }

        public ProductSubCategory SubCategoryById(SqlConnection db, long Id)
        {
            return new ProductSubCategory
            {
                Id = Id,
                Name = "test",
                ProductCategoryID = 1
            };
        }

        public List<ProductSubCategory> SubCategoryList(SqlConnection db, long categID, int skip, int take, ref int total)
        {
            throw new System.NotImplementedException();
        }
    }
}
