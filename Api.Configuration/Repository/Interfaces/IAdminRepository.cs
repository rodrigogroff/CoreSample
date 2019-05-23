using Entities.Api.Configuration;
using System.Data.SqlClient;

namespace Api.Configuration.Repository
{
    public interface IAdminRepository
    {        
        bool AdminExists(SqlConnection db, string email );
        bool AdminLogin(SqlConnection db, string email, string password, ref Entities.Database.User user);
        long AddAdmin(SqlConnection db, NewUserData user);
    }
}
