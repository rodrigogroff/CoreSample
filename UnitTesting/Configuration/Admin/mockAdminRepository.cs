using Api.Configuration.Repository;
using Entities.Api.Configuration;
using Entities.Database;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace UnitTesting
{
    public class mockAdminRepository : IAdminRepository
    {
        #region - mock methods - 

        public long AdminAdd(SqlConnection db, NewUserData user)
        {
            return 1;
        }

        public bool AdminExists(SqlConnection db, string email)
        {
            if (email == "pass@test.com")
                return false;

            return true;
        }

        public bool AdminLogin(SqlConnection db, string email, string password, ref User user)
        {
            return true;
        }

        public bool CategoryExists(SqlConnection db, string name)
        {
            if (name == "pass")
                return false;

            return true;
        }

        public long CategoryAdd(SqlConnection db, NewCategoryData obj)
        {
            return 1;
        }

        public bool CategoryExistsId(SqlConnection db, long id)
        {
            return true;
        }

        public void CategoryEdit(SqlConnection db, NewCategoryData obj)
        {
            
        }

        public List<ProductCategory> CategoryList(SqlConnection db, int skip, int take, ref int total)
        {
            total = 1;
            return new List<ProductCategory>
            {
                new ProductCategory
                {
                    Id  = 1,
                    Name = "teste"
                }
            };
        }

        public ProductCategory CategoryById(SqlConnection db, long Id)
        {
            if (Id == 0)
                return null;

            return new ProductCategory
            {
                Id = 1,
                Name = "test"
            };
        }

        public long SubCategoryAdd(SqlConnection db, NewSubCategoryData obj)
        {
            return 1;
        }

        public bool SubCategoryExists(SqlConnection db, long pcID, string name)
        {
            return false;
        }

        public List<ProductSubCategory> SubCategoryList(SqlConnection db, long categID, int skip, int take, ref int total)
        {
            total = 1;
            return new List<ProductSubCategory>
            {
                new ProductSubCategory
                {
                    Id  = 1,
                    Name = "teste"
                }
            };
        }

        public bool SubCategoryExistsId(SqlConnection db, long id)
        {
            return true;
        }

        public void SubCategoryEdit(SqlConnection db, NewSubCategoryData obj)
        {
            
        }

        public ProductSubCategory SubCategoryById(SqlConnection db, long Id)
        {
            if (Id == 0)
                return null;

            return new ProductSubCategory
            {
                Id = Id,
                Name = "test"
            };
        }

        public bool ProductExists(SqlConnection db, long pcID, long subID, string name)
        {
            if (pcID == 1 && subID == 1)
                return true;

            return false;
        }

        public long ProductAdd(SqlConnection db, Product obj)
        {
            return 1;
        }

        public bool ProductExistsId(SqlConnection db, long id)
        {
            if (id == 0)
                return false;

            return true;
        }

        public void ProductEdit(SqlConnection db, NewProductData obj)
        {
            throw new System.NotImplementedException();
        }

        public Product ProductById(SqlConnection db, long Id)
        {
            throw new System.NotImplementedException();
        }

        public void ProductEdit(SqlConnection db, Product obj)
        {
            throw new System.NotImplementedException();
        }

        public Admin AdminById(SqlConnection db, long Id)
        {
            throw new System.NotImplementedException();
        }

        #endregion
    }
}
