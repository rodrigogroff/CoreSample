using Master.Controllers;
using System.Data.SqlClient;

namespace Api.User.Repository
{
    public interface IUserRepository
    {
        bool UserExists(SqlConnection db, string name);
        long AddUser(SqlConnection db, NewUserData user);
    }
}
