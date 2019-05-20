using Gateway.Controllers;
using System.Data.SqlClient;
using Dapper;
using System.Linq;

namespace Api.User.Domain
{
    public class CreateAccountV1
    {
        public ServiceError Error = new ServiceError();
        
        public bool CreateAccount(NewUserData newUser, SqlConnection db)
        {
            if (string.IsNullOrEmpty(newUser.Name))
            {
                Error.Message = "Name is empty!";
                return false;
            }

            if (string.IsNullOrEmpty(newUser.Email))
            {
                Error.Message = "Email is empty!";
                return false;
            }
            
            if (db.QueryFirstOrDefault<long>("select Id from [User] (nolock) where Name=@NewName", new { NewName = newUser.Name }) > 0)
            {
                Error.Message = "Name already taken into our database";
                return false;
            }

            var user = new User
            {
                Name = newUser.Name,
                Email = newUser.Email,                
                Phone = newUser.Phone,
                Password = "123456",
            };
            
            string sql = @"INSERT INTO [User] (Name,Email,Phone,Password) VALUES 
                          (@Name,@Email,@Phone,@Password); 
                          SELECT CAST(SCOPE_IDENTITY() as bigint)";

            user.Id = db.Query<long>(sql, new { user.Name, user.Email, user.Phone, user.Password }).Single();
            
            return true;
        }
    }
}
