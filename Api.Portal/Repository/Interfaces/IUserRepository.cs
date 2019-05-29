using Entities.Api.Configuration;
using Entities.Api.Portal;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace Api.Portal.Repository
{
    public interface IUserRepository
    {        
        bool UserExists(SqlConnection db, string email );
        bool UserLogin(SqlConnection db, string email, string password, ref Entities.Database.User user);
        long UserAdd(SqlConnection db, NewUserData user);
        List<Entities.Database.ProductComment> UserComments(SqlConnection db, long userId, int skip, int take, ref int total);
    }
}
