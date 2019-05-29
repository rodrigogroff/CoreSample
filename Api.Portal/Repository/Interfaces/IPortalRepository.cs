using Entities.Database;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace Api.Portal.Repository
{
    public interface IPortalRepository
    {
        Entities.Database.Product ProductById(SqlConnection db, long Id);
        List<Entities.Database.ProductCategory> CategoryList(SqlConnection db, int skip, int take, ref int total);
        List<Entities.Database.ProductSubCategory> SubCategoryList(SqlConnection db, long categID, int skip, int take, ref int total);
        long ProductAddView(SqlConnection db, long userId, long productId);
        long ProductViews(SqlConnection db, long productId);

        ProductCategory CategoryById(SqlConnection db, long Id);
        ProductSubCategory SubCategoryById(SqlConnection db, long Id);
        List<Entities.Database.Product> ProductList(SqlConnection db, long categID, long subcategID, int skip, int take, ref int total);
    }
}
