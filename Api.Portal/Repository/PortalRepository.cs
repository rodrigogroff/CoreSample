using Dapper;
using Entities.Database;
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

            return db.Query<Entities.Database.ProductCategory>("select * from [ProductCategory] order by Name desc " +
                                                               "offset @skip rows fetch next @take rows only", new { skip, take }).
                                                               ToList();
        }

        public List<ProductSubCategory> SubCategoryList(SqlConnection db, long categID, int skip, int take, ref int total)
        {
            total = db.Query<int>("select count(*) from [ProductSubCategory] where ProductCategoryID=@categID", new { categID }).Single();

            return db.Query<Entities.Database.ProductSubCategory>(  @"select * from [ProductSubCategory] 
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
    }
}
