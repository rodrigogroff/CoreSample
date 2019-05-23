using Master.Controllers;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace Api.Configuration.Repository
{
    public interface IUserRepository
    {        
        bool UserExists(SqlConnection db, string email );
        bool UserLogin(SqlConnection db, string email, string password, ref Database.User user);
        long AddUser(SqlConnection db, NewUserData user);
        List<Database.ProductComment> UserComments(SqlConnection db, long userId, int skip, int take, ref int total);
    }
}
