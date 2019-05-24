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
        bool CategoryExists(SqlConnection db, long id);
        long CategoryAdd(SqlConnection db, NewCategoryData obj);
        void CategoryEdit(SqlConnection db, NewCategoryData obj);
        List<Entities.Database.ProductCategory> CategoryList(SqlConnection db, int skip, int take, ref int total);
        Entities.Database.ProductCategory CategoryById(SqlConnection db, long Id);
    }
}
