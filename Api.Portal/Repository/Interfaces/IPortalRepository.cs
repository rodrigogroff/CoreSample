using System.Collections.Generic;
using System.Data.SqlClient;

namespace Api.Portal.Repository
{
    public interface IPortalRepository
    {
        List<Entities.Database.ProductCategory> CategoryList(SqlConnection db, int skip, int take, ref int total);

        List<Entities.Database.ProductSubCategory> SubCategoryList(SqlConnection db, long categID, int skip, int take, ref int total);
    }
}
