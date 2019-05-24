using Api.Configuration.Repository;
using Entities.Api.Configuration;
using Entities.Database;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace UnitTesting
{
    public class mockAdminRepositoryUserExists : IAdminRepository
    {
        #region - mock methods - 

        public long AdminAdd(SqlConnection db, NewUserData user)
        {
            return 1;
        }

        public bool AdminExists(SqlConnection db, string email)
        {
            return true;
        }

        public bool AdminLogin(SqlConnection db, string email, string password, ref User user)
        {
            return true;
        }

        public bool CategoryExists(SqlConnection db, string name)
        {
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

        #endregion
    }

    public class mockAdminRepositoryUserNotExists : IAdminRepository
    {
        #region - mock methods - 

        public long AdminAdd(SqlConnection db, NewUserData user)
        {
            return 1;
        }

        public bool AdminExists(SqlConnection db, string email)
        {
            return false;
        }

        public bool AdminLogin(SqlConnection db, string email, string password, ref User user)
        {
            return false;
        }

        public bool CategoryExists(SqlConnection db, string name)
        {
            return false;
        }

        public long CategoryAdd(SqlConnection db, NewCategoryData obj)
        {
            return 1;
        }

        public bool CategoryExists(SqlConnection db, string name, long id)
        {
            return false;
        }

        public bool CategoryExistsId(SqlConnection db, long id)
        {
            return false;
        }

        public void CategoryEdit(SqlConnection db, NewCategoryData obj)
        {
            
        }

        public List<ProductCategory> CategoryList(SqlConnection db, int skip, int take, ref int total)
        {
            return new List<ProductCategory>
            {
                new ProductCategory
                {
                    Id  = 1,
                    Name = "test"
                }
            };
        }

        public ProductCategory CategoryById(SqlConnection db, long Id)
        {
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
            throw new System.NotImplementedException();
        }

        public List<ProductSubCategory> SubCategoryList(SqlConnection db, long categID, int skip, int take, ref int total)
        {
            throw new System.NotImplementedException();
        }

        #endregion
    }
}
