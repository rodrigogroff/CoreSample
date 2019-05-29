using Api.Portal.Repository;
using Entities.Api.Configuration;
using Entities.Database;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace UnitTesting
{
    public class mockPortalRepository : IPortalRepository
    {
        public ProductCategory CategoryById(SqlConnection db, long Id)
        {
            throw new System.NotImplementedException();
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
            throw new System.NotImplementedException();
        }

        public Product ProductById(SqlConnection db, long Id)
        {
            throw new System.NotImplementedException();
        }

        public long ProductViews(SqlConnection db, long productId)
        {
            throw new System.NotImplementedException();
        }

        public ProductSubCategory SubCategoryById(SqlConnection db, long Id)
        {
            throw new System.NotImplementedException();
        }

        public List<ProductSubCategory> SubCategoryList(SqlConnection db, long categID, int skip, int take, ref int total)
        {
            throw new System.NotImplementedException();
        }
    }
}
