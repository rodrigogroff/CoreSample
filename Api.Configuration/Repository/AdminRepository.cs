﻿using Dapper;
using Entities.Api.Configuration;
using System.Data.SqlClient;
using System.Linq;

namespace Api.Configuration.Repository
{
    public class AdminRepository : IAdminRepository
    {        
        public bool AdminExists(SqlConnection db, string email)
        {
            return db.QueryFirstOrDefault<long>
                ("select Id from [Admin] (nolock) where Email=@NewEmail", new
                {
                    NewEmail = email                    
                }) > 0;
        }

        public long AddAdmin(SqlConnection db, NewUserData user)
        {
            string sql = @"INSERT INTO [Admin] 
                          (Name,Email,Phone,Password) 
                          VALUES 
                          (@Name,@Email,@Phone,@Password); 
                          SELECT CAST(SCOPE_IDENTITY() as bigint)";

            return db.Query<long>(sql, new { user.Name, user.Email, user.Phone, user.Password }).Single();
        }

        public bool AdminLogin(SqlConnection db, string email, string password, ref Entities.Database.User user_db)
        {
            user_db = db.QueryFirstOrDefault<Entities.Database.User >
                        ("select * from [Admin] (nolock) where Email=@email and Password=@password", new
                        {
                            email,
                            password,
                        });

            return user_db != null;
        }

        public bool CategoryExists(SqlConnection db, string name)
        {
            return db.QueryFirstOrDefault<long>
                ("select Id from [ProductCategory] (nolock) where Name=@NewName", new
                {
                    NewName = name
                }) > 0;
        }

        public long AddCategory(SqlConnection db, NewCategoryData obj)
        {
            string sql = @"INSERT INTO [ProductCategory] 
                          (Name) 
                          VALUES 
                          (@Name); 
                          SELECT CAST(SCOPE_IDENTITY() as bigint)";

            return db.Query<long>(sql, new { obj.Name }).Single();
        }
    }
}
