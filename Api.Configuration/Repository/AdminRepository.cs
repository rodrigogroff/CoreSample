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
                ("select Id from [Admin] (nolock) where UPPER(Email)=@email", new
                {
                    email = email.ToUpper()
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

        public bool AdminLogin(SqlConnection db, string email, string password, ref User user_db)
        {
            user_db = db.QueryFirstOrDefault<User >
                        ("select * from [Admin] (nolock) where UPPER(Email)=@email and Password=@password", new
                        {
                            email = email.ToUpper(),
                            password,
                        });

            return user_db != null;
        }

        public bool CategoryExists(SqlConnection db, string name)
        {
            return db.QueryFirstOrDefault<long>
                ("select Id from [ProductCategory] (nolock) where UPPER(Name)=@name", new
                {
                    name = name.ToUpper()
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

        public List<ProductCategory> CategoryList(SqlConnection db, int skip, int take, ref int total)
        {
            total = db.Query<int>("select count(*) from [ProductCategory]").Single();

            return db.Query<ProductCategory>(@"select * from [ProductCategory] order by Name desc 
                                               offset @skip rows fetch next @take rows only", new { skip, take } ).
                                               ToList();
        }

        public ProductCategory CategoryById(SqlConnection db, long Id)
        {
            return db.Query<ProductCategory>("select * from [ProductCategory] where Id=@Id", new { Id }).FirstOrDefault();
        }

        public bool SubCategoryExists(SqlConnection db, long pcID, string name)
        {
            return db.QueryFirstOrDefault<long>
                ("select Id from [ProductSubCategory] (nolock) where UPPER(Name)=@name and ProductCategoryID=@pcID", new
                {
                    name = name.ToUpper(),
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

        public List<ProductSubCategory> SubCategoryList(SqlConnection db, long categID, int skip, int take, ref int total)
        {
            total = db.Query<int>("select count(*) from [ProductSubCategory] where ProductCategoryID=@categID", new { categID }).Single();

            return db.Query<ProductSubCategory>(@"select * from [ProductSubCategory] 
                                                  where ProductCategoryID=@categID      
                                                  order by Name desc offset @skip rows fetch next @take rows only", new { categID, skip, take }).
                                                  ToList();
        }

        public void SubCategoryEdit(SqlConnection db, NewSubCategoryData obj)
        {
            db.Query(@"update [ProductSubCategory] set Name=@Name where Id=@Id", new { obj.Name, obj.Id });
        }

        public bool SubCategoryExistsId(SqlConnection db, long id)
        {
            return db.QueryFirstOrDefault<long>
               ("select Id from [ProductSubCategory] (nolock) where Id=@id", new
               {
                   id
               }) > 0;
        }

        public ProductSubCategory SubCategoryById(SqlConnection db, long Id)
        {
            return db.Query<ProductSubCategory>("select * from [ProductSubCategory] where Id=@Id", new { Id }).FirstOrDefault();
        }

        public bool ProductExists(SqlConnection db, long pcID, long subID, string name)
        {
            return db.QueryFirstOrDefault<long>
                            ("select Id from [Product] (nolock) where UPPER(Name)=@name and ProductCategoryID=@pcID and ProductSubCategoryID=@subID", new
                            {
                                name = name.ToUpper(),
                                pcID,
                                subID
                            }) > 0;
        }

        public long ProductAdd(SqlConnection db, Product obj)
        {
            string sql = @"INSERT INTO [Product] 
                          (ProductCategoryID,ProductSubCategoryID,Name,CreatedByAdminID,DateAdded,LastEditByAdminID,DateEdit) 
                          VALUES 
                          (@ProductCategoryID,@ProductSubCategoryID,@Name,@CreatedByAdminID,@DateAdded,@LastEditByAdminID,@DateEdit); 
                          SELECT CAST(SCOPE_IDENTITY() as bigint)";

            return db.Query<long>(sql, obj).Single();
        }

        public bool ProductExistsId(SqlConnection db, long id)
        {
            return db.QueryFirstOrDefault<long>
                ("select Id from [Product] (nolock) where Id=@id", new
                {
                    id
                }) > 0;
        }

        public void ProductEdit(SqlConnection db, NewProductData obj)
        {
            db.Query(@"update [Product] set Name=@Name where Id=@Id", new { obj.Name, obj.Id });
        }

        public Product ProductById(SqlConnection db, long Id)
        {
            return db.Query<Product>("select * from [Product] where Id=@Id", new { Id }).FirstOrDefault();
        }

        public void ProductEdit(SqlConnection db, Product obj)
        {
            db.Query(@"update [Product] set Name=@Name,DateEdit=@DateEdit,LastEditByAdminID=@LastEditByAdminID where Id=@Id", new
            {
                obj.Name,
                obj.DateEdit,
                obj.LastEditByAdminID,
                obj.Id
            });
        }
    }
}
