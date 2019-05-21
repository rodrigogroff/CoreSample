using Dapper;
using Master.Controllers;
using System.Data.SqlClient;
using System.Linq;

namespace Api.User.Repository
{
    public class UserRepository : IUserRepository
    {
        public bool ClientExists(SqlConnection db, long clientID)
        {
            return db.QueryFirstOrDefault<long>
                ("select Id from [Client] (nolock) where Id=@Client", new
                {
                    Client = clientID
                }) > 0;
        }

        public bool UserExists(SqlConnection db, string email, long clientID)
        {
            return db.QueryFirstOrDefault<long>
                ("select Id from [User] (nolock) where Email=@NewEmail and ClientID=@Client", new
                {
                    NewEmail = email,
                    Client = clientID
                }) > 0;
        }

        public long AddUser(SqlConnection db, NewUserData user)
        {
            string sql = @"INSERT INTO [User] 
                          (Name,Email,Phone,Password,ClientID) 
                          VALUES 
                          (@Name,@Email,@Phone,@Password,@ClientID); 
                          SELECT CAST(SCOPE_IDENTITY() as bigint)";

            return db.Query<long>(sql, new { user.Name, user.Email, user.Phone, user.Password, user.ClientID }).Single();
        }
    }
}
