using Entities.Api.Portal;
using Entities.Database;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace Api.Portal.Repository
{
    public interface IPortalRepository
    {
        Product ProductById(SqlConnection db, long Id);
        List<ProductCategory> CategoryList(SqlConnection db, int skip, int take, ref int total);
        List<ProductSubCategory> SubCategoryList(SqlConnection db, long categID, int skip, int take, ref int total);
        long ProductAddView(SqlConnection db, long userId, long productId);
        long ProductViews(SqlConnection db, long productId);
        long ProductAddComment(SqlConnection db, long userId, NewProductComment comment);
        List<ProductComment> ProductComments(SqlConnection db, long productID);

        User UserById(SqlConnection db, long? Id);
        ProductCategory CategoryById(SqlConnection db, long Id);
        ProductSubCategory SubCategoryById(SqlConnection db, long Id);
        List<Product> ProductList(SqlConnection db, long categID, long subcategID, int skip, int take, ref int total);
    }
}
