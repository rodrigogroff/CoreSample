using Entities.Api.Configuration;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace Api.Configuration.Repository
{
    public interface IAdminRepository
    {
        bool AdminExists(SqlConnection db, string email );
        bool AdminLogin(SqlConnection db, string email, string password, ref Entities.Database.User user);
        long AdminAdd(SqlConnection db, NewUserData user);
        
        bool CategoryExists(SqlConnection db, string name);
        bool CategoryExistsId(SqlConnection db, long id);
        long CategoryAdd(SqlConnection db, NewCategoryData obj);
        void CategoryEdit(SqlConnection db, NewCategoryData obj);
        List<Entities.Database.ProductCategory> CategoryList(SqlConnection db, int skip, int take, ref int total);
        Entities.Database.ProductCategory CategoryById(SqlConnection db, long Id);

        bool SubCategoryExists(SqlConnection db, long pcID, string name);
        bool SubCategoryExistsId(SqlConnection db, long id);
        long SubCategoryAdd(SqlConnection db, NewSubCategoryData obj);
        List<Entities.Database.ProductSubCategory> SubCategoryList(SqlConnection db, long categID, int skip, int take, ref int total);
        void SubCategoryEdit(SqlConnection db, NewSubCategoryData obj);
    }
}
