using Api.Configuration.Repository;
using Entities.Api.Configuration;
using Entities.Database;
using System.Data.SqlClient;

namespace UnitTesting
{
    public class mockAdminRepositoryUserExists : IAdminRepository
    {
        public long AddAdmin(SqlConnection db, NewUserData user)
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
    }

    public class mockAdminRepositoryUserNotExists : IAdminRepository
    {
        public long AddAdmin(SqlConnection db, NewUserData user)
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
    }
}
