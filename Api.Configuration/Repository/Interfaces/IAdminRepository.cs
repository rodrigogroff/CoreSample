using Entities.Api.Configuration;
using Entities.Database;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace Api.Configuration.Repository
{
    public interface IAdminRepository
    {
        bool AdminExists(SqlConnection db, string email );
        bool AdminLogin(SqlConnection db, string email, string password, ref User user);
        long AdminAdd(SqlConnection db, NewUserData user);
        Admin AdminById(SqlConnection db, long Id);

        bool CategoryExists(SqlConnection db, string name);
        bool CategoryExistsId(SqlConnection db, long id);
        long CategoryAdd(SqlConnection db, NewCategoryData obj);
        void CategoryEdit(SqlConnection db, NewCategoryData obj);
        List<ProductCategory> CategoryList(SqlConnection db, int skip, int take, ref int total);
        ProductCategory CategoryById(SqlConnection db, long Id);

        bool SubCategoryExists(SqlConnection db, long pcID, string name);
        bool SubCategoryExistsId(SqlConnection db, long id);
        long SubCategoryAdd(SqlConnection db, NewSubCategoryData obj);
        List<ProductSubCategory> SubCategoryList(SqlConnection db, long categID, int skip, int take, ref int total);
        void SubCategoryEdit(SqlConnection db, NewSubCategoryData obj);
        ProductSubCategory SubCategoryById(SqlConnection db, long Id);

        bool ProductExists(SqlConnection db, long pcID, long subID, string name);
        long ProductAdd(SqlConnection db, Product obj);
        bool ProductExistsId(SqlConnection db, long id);
        Product ProductById(SqlConnection db, long Id);
        void ProductEdit(SqlConnection db, Product obj);
        List<Product> ProductList(SqlConnection db, long categID, long subcategID, int skip, int take, ref int total);
    }
}
