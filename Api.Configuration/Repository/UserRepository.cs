using Dapper;
using Database;
using Master.Controllers;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

namespace Api.User.Repository
{
    public class UserRepository : IUserRepository
    {        
        public bool UserExists(SqlConnection db, string email)
        {
            return db.QueryFirstOrDefault<long>
                ("select Id from [User] (nolock) where Email=@NewEmail", new
                {
                    NewEmail = email                    
                }) > 0;
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

        public bool UserLogin(SqlConnection db, string email, string password, ref Database.User user_db)
        {
            user_db = db.QueryFirstOrDefault<Database.User>
                        ("select * from [User] (nolock) where Email=@email and Password=@password", new
                        {
                            email,
                            password,
                        });

            return user_db != null;
        }

        public List<ProductComment> UserComments(SqlConnection db, long userId, int skip, int take, ref int total)
        {
            total = db.Query<int>("select count(*) from [ProductComment] where UserID = @userId", new { userId }).Single();

            return db.Query<ProductComment>("select * from [ProductComment] where UserID = @userId order by DateAdded desc " +
                                            "offset " +  skip + " rows fetch next " + take + " rows only", 
                                            new { userId }).ToList();
        }
    }
}
