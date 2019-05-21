using Master.Controllers;
using System.Data.SqlClient;

namespace Api.User.Repository
{
    public interface IUserRepository
    {
        bool ClientExists(SqlConnection db, string clientGuid);
        bool UserExists(SqlConnection db, string name, string clientGuid);
        long AddUser(SqlConnection db, NewUserData user);
    }
}
