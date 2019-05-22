using Dapper;
using Database;
using Master.Controllers;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

namespace Api.User.Repository
{
    public class UserRepository : IUserRepository
    {
        public bool ClientExists(SqlConnection db, string client_guid)
        {
            return db.QueryFirstOrDefault<long> ("select Id from [Client] (nolock) where Guid=@Client", new { Client = client_guid }) > 0;
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
            var clientId =  db.QueryFirstOrDefault<long?> ("select Id from [Client] (nolock) where Guid=@Client", new { Client = user.ClientGUID });

            if (clientId == null)
                throw (new Exception("Invalid Client Credential"));

            string sql = @"INSERT INTO [User] 
                          (Name,Email,Phone,Password,ClientID) 
                          VALUES 
                          (@Name,@Email,@Phone,@Password,@ClientID); 
                          SELECT CAST(SCOPE_IDENTITY() as bigint)";

            return db.Query<long>(sql, new { user.Name, user.Email, user.Phone, user.Password, ClientID = (long)clientId }).Single();
        }

        public bool UserLogin(SqlConnection db, string email, string password, string clientGuid, ref Database.User user_db)
        {
            user_db = db.QueryFirstOrDefault<Database.User>
                        ("select * from [User] (nolock) where Email=@email and Password=@password and " +
                        "ClientID in ( select Id from [Client] (nolock) where Guid = @clientGuid )", new
                        {
                            email,
                            password,
                            clientGuid
                        });

            return user_db != null;
        }

        public List<ProductComment> UserComments(SqlConnection db, long userId, int skip, int take, ref int total)
        {
            total = db.Query<int>("select count(*) from [ProductComment] where UserID = @userId", new { userId }).Single();

            return db.Query<ProductComment>("select * from [ProductComment] where UserID = @userId " +
                                            "order by DateAdded desc offset " +  skip + " rows fetch next " + take + " rows only", 
                                            new { userId }).ToList();
        }
    }
}
