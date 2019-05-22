using Master.Controllers;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace Api.User.Repository
{
    public interface IUserRepository
    {
        bool ClientExists(SqlConnection db, string clientGuid);
        bool UserExists(SqlConnection db, string name, string clientGuid);
        bool UserLogin(SqlConnection db, string email, string password, string clientGuid, ref Database.User user);
        long AddUser(SqlConnection db, NewUserData user);
        List<Database.ProductComment> UserComments(SqlConnection db, long userId, int skip, int take, ref int total);
    }
}
