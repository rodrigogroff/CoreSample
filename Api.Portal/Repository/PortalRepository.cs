using Dapper;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

namespace Api.Portal.Repository
{
    public class PortalRepository : IPortalRepository
    {        
        public List<Entities.Database.ProductCategory> CategoryList(SqlConnection db, int skip, int take, ref int total)
        {
            total = db.Query<int>("select count(*) from [ProductCategory]").Single();

            return db.Query<Entities.Database.ProductCategory>("select * from [ProductCategory] order by Name desc " +
                                                               "offset " + skip + " rows fetch next " + take + " rows only" ).
                                                               ToList();
        }
    }
}
