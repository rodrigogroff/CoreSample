using Dapper;
using Entities.Database;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

namespace Api.Portal.Repository
{
    public class PortalRepository : IPortalRepository
    {        
        public List<ProductCategory> CategoryList(SqlConnection db, int skip, int take, ref int total)
        {
            total = db.Query<int>("select count(*) from [ProductCategory]").Single();

            return db.Query<ProductCategory>(@"select * from [ProductCategory] order by Name desc 
                                               offset @skip rows fetch next @take rows only", new { skip, take }).
                                               ToList();
        }

        
        public List<ProductSubCategory> SubCategoryList(SqlConnection db, long categID, int skip, int take, ref int total)
        {
            total = db.Query<int>("select count(*) from [ProductSubCategory] where ProductCategoryID=@categID", new { categID }).Single();

            return db.Query<ProductSubCategory>(  @"select * from [ProductSubCategory] 
                                                    where ProductCategoryID=@categID 
                                                    order by Name desc 
                                                    offset @skip rows fetch next @take rows only", new
                                                {
                                                    categID,
                                                    skip,
                                                    take,
                                                }).
                                                ToList();
        }

        public Product ProductById(SqlConnection db, long Id)
        {
            return db.Query<Product>("select * from [Product] where Id=@Id", new { Id }).FirstOrDefault();
        }

        public long ProductAddView(SqlConnection db, long userId, long productId)
        {
            string sql = @"INSERT INTO [ProductView] 
                 (ProductID,UserID,DateAdded) 
                 VALUES 
                 (@ProductID,@UserID,@DateAdded); 
                 SELECT CAST(SCOPE_IDENTITY() as bigint)";

            return db.Query<long>(sql, new { ProductID = productId, UserID = userId, DateAdded = DateTime.Now }).Single();
        }

        public long ProductViews(SqlConnection db, long productId)
        {
            return db.Query<long>("select count(*) from [ProductView] where ProductID=@productId", new { productId }).Single();
        }

        public ProductCategory CategoryById(SqlConnection db, long Id)
        {
            return db.Query<ProductCategory>("select * from [ProductCategory] where Id=@Id", new { Id }).FirstOrDefault();
        }

        public ProductSubCategory SubCategoryById(SqlConnection db, long Id)
        {
            return db.Query<ProductSubCategory>("select * from [ProductSubCategory] where Id=@Id", new { Id }).FirstOrDefault();
        }
    }
}
