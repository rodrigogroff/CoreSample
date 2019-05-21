using Master.Controllers;
using System.Data.SqlClient;

namespace Api.User.Repository
{
    public interface IUserRepository
    {
        bool ClientExists(SqlConnection db, long clientID);
        bool UserExists(SqlConnection db, string name, long clientID);
        long AddUser(SqlConnection db, NewUserData user);
    }
}
