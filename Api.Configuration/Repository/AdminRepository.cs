using Dapper;
using Entities.Api.Configuration;
using Master.Controllers;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

namespace Api.Configuration.Repository
{
    public class AdminRepository : IAdminRepository
    {        
        public bool UserExists(SqlConnection db, string email)
        {
            return db.QueryFirstOrDefault<long>
                ("select Id from [Admin] (nolock) where Email=@NewEmail", new
                {
                    NewEmail = email                    
                }) > 0;
        }

        public long AddUser(SqlConnection db, NewUserData user)
        {
            string sql = @"INSERT INTO [Admin] 
                          (Name,Email,Phone,Password) 
                          VALUES 
                          (@Name,@Email,@Phone,@Password); 
                          SELECT CAST(SCOPE_IDENTITY() as bigint)";

            return db.Query<long>(sql, new { user.Name, user.Email, user.Phone, user.Password }).Single();
        }

        public bool UserLogin(SqlConnection db, string email, string password, ref Entities.Database.User user_db)
        {
            user_db = db.QueryFirstOrDefault<Entities.Database.User >
                        ("select * from [Admin] (nolock) where Email=@email and Password=@password", new
                        {
                            email,
                            password,
                        });

            return user_db != null;
        }
    }
}
