using Api.User.Repository;
using Gateway.Controllers;
using System.Data.SqlClient;

namespace UnitTesting
{
    public class mockUserRepositoryUserExists : IUserRepository
    {
        public long AddUser(SqlConnection db, NewUserData user)  { return 1; }

        public bool UserExists(SqlConnection db, string name) { return true; }
    }

    public class mockUserRepositoryUserNotExists : IUserRepository
    {
        public long AddUser(SqlConnection db, NewUserData user) { return 1; }

        public bool UserExists(SqlConnection db, string name) { return false; }
    }
}