using Dapper;
using Master.Controllers;
using System;
using System.Data.SqlClient;
using System.Linq;

namespace Api.User.Repository
{
    public class UserRepository : IUserRepository
    {
        public bool ClientExists(SqlConnection db, string client_guid)
        {
            return db.QueryFirstOrDefault<long>
                ("select Id from [Client] (nolock) where Guid=@Client", new
                {
                    Client = client_guid
                }) > 0;
        }

        public bool UserExists(SqlConnection db, string email, string client_guid)
        {
            return db.QueryFirstOrDefault<long>
                ("select Id from [User] (nolock) where Email=@NewEmail and ClientID in ( select Id from [Client] where Guid = @Client )", new
                {
                    NewEmail = email,
                    Client = client_guid
                }) > 0;
        }

        public long AddUser(SqlConnection db, NewUserData user)
        {
            var clientId =  db.QueryFirstOrDefault<long?>
                            ("select Id from [Client] (nolock) where Guid=@Client", new
                            {
                                Client = user.ClientGUID
                            });

            if (clientId == null)
                throw (new Exception("Invalid Client Credential"));

            string sql = @"INSERT INTO [User] 
                          (Name,Email,Phone,Password,ClientID) 
                          VALUES 
                          (@Name,@Email,@Phone,@Password,@ClientID); 
                          SELECT CAST(SCOPE_IDENTITY() as bigint)";

            return db.Query<long>(sql, new { user.Name, user.Email, user.Phone, user.Password, ClientID = (long)clientId }).Single();
        }
    }
}
