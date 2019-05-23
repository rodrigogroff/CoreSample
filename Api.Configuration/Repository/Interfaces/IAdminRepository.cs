using Entities.Api.Configuration;
using System.Data.SqlClient;

namespace Api.Configuration.Repository
{
    public interface IAdminRepository
    {        
        bool UserExists(SqlConnection db, string email );
        bool UserLogin(SqlConnection db, string email, string password, ref Entities.Database.User user);
        long AddUser(SqlConnection db, NewUserData user);
    }
}
