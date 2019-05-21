using Dapper;
using Master.Controllers;
using System.Data.SqlClient;
using System.Linq;

namespace Api.User.Repository
{
    public class UserRepository : IUserRepository
    {        
        public bool UserExists(SqlConnection db, string email)
        {
            return db.QueryFirstOrDefault<long>("select Id from [User] (nolock) where Email=@NewEmail", new { NewEmail = email }) > 0;
        }

        public long AddUser(SqlConnection db, NewUserData user)
        {
            string sql = @"INSERT INTO [User] 
                          (Name,Email,Phone,Password) 
                          VALUES 
                          (@Name,@Email,@Phone,@Password); 
                          SELECT CAST(SCOPE_IDENTITY() as bigint)";

            return db.Query<long>(sql, new { user.Name, user.Email, user.Phone, user.Password }).Single();
        }
    }
}
