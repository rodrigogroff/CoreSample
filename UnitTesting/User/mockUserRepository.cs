using Api.User.Repository;
using Database;
using Master.Controllers;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace UnitTesting
{
    public class mockUserRepositoryUserExists : IUserRepository
    {
        public long AddUser(SqlConnection db, NewUserData user)  { return 1; }
        
        public bool UserExists(SqlConnection db, string email ) { return true; }

        public bool UserLogin(SqlConnection db, string email, string password, ref User user) { return true; }

        public List<ProductComment> UserComments(SqlConnection db, long userId, int skip, int take, ref int total)
        {
            total = 1;

            return new List<ProductComment>
            {
                new ProductComment
                {
                    Comment = "oi"
                }
            };
        }
    }

    public class mockUserRepositoryUserNotExists : IUserRepository
    {
        public long AddUser(SqlConnection db, NewUserData user) { return 1; }

        public bool UserExists(SqlConnection db, string email ) { return false; }

        public bool UserLogin(SqlConnection db, string email, string password, ref User user) { return false; }

        public List<ProductComment> UserComments(SqlConnection db, long userId, int skip, int take, ref int total)
        {
            throw new System.NotImplementedException();
        }
    }
}
