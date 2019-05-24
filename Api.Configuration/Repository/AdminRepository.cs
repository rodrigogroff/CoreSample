using Dapper;
using Entities.Api.Configuration;
using Entities.Database;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

namespace Api.Configuration.Repository
{
    public class AdminRepository : IAdminRepository
    {        
        public bool AdminExists(SqlConnection db, string email)
        {
            return db.QueryFirstOrDefault<long>
                ("select Id from [Admin] (nolock) where Email=@email", new
                {
                    email                    
                }) > 0;
        }

        public long AdminAdd(SqlConnection db, NewUserData user)
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
                ("select Id from [ProductCategory] (nolock) where Name=@name", new
                {
                    name
                }) > 0;
        }

        public bool CategoryExistsId(SqlConnection db, long id)
        {
            return db.QueryFirstOrDefault<long>
                ("select Id from [ProductCategory] (nolock) where Id=@id", new
                {
                    id
                }) > 0;
        }

        public long CategoryAdd(SqlConnection db, NewCategoryData obj)
        {
            string sql = @"INSERT INTO [ProductCategory] 
                          (Name) 
                          VALUES 
                          (@Name); 
                          SELECT CAST(SCOPE_IDENTITY() as bigint)";

            return db.Query<long>(sql, new { obj.Name }).Single();
        }

        public void CategoryEdit(SqlConnection db, NewCategoryData obj)
        {
            db.Query(@"update [ProductCategory] set Name=@Name where Id=@Id", new { obj.Name, obj.Id });
        }

        public List<Entities.Database.ProductCategory> CategoryList(SqlConnection db, int skip, int take, ref int total)
        {
            total = db.Query<int>("select count(*) from [ProductCategory]").Single();

            return db.Query<Entities.Database.ProductCategory>("select * from [ProductCategory] order by Name desc " +
                                                               "offset " + skip + " rows fetch next " + take + " rows only" ).
                                                               ToList();
        }

        public ProductCategory CategoryById(SqlConnection db, long Id)
        {
            return db.Query<Entities.Database.ProductCategory>("select * from [ProductCategory] where Id=@Id", new { Id }).FirstOrDefault();
        }

        public bool SubCategoryExists(SqlConnection db, long pcID, string name)
        {
            return db.QueryFirstOrDefault<long>
                ("select Id from [ProductSubCategory] (nolock) where Name=@name and ProductCategoryID=@pcID", new
                {
                    name,
                    pcID
                }) > 0;
        }

        public long SubCategoryAdd(SqlConnection db, NewSubCategoryData obj)
        {
            string sql = @"INSERT INTO [ProductSubCategory] 
                          (ProductCategoryID,Name) 
                          VALUES 
                          (@ProductCategoryID,@Name); 
                          SELECT CAST(SCOPE_IDENTITY() as bigint)";

            return db.Query<long>(sql, new
            {
                obj.ProductCategoryID,
                obj.Name
            }).
            Single();
        }        
    }
}
