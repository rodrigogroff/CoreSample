using Api.Configuration.Repository;
using Entities.Api.Configuration;
using Entities.Database;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace UnitTesting
{
    public class mockAdminRepositoryUserExists : IAdminRepository
    {
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

        public bool CategoryExists(SqlConnection db, long id)
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
    }

    public class mockAdminRepositoryUserNotExists : IAdminRepository
    {
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

        public bool CategoryExists(SqlConnection db, long id)
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
                    Name = "rodrigo"
                }
            };
        }
    }
}
