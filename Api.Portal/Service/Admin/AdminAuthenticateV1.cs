using Api.Configuration.Repository;
using Entities.Api;
using Entities.Api.Configuration;
using System.Data.SqlClient;

namespace Api.Configuration.Service
{
    public class AdminAuthenticateV1
    {
        public ServiceError Error;        
        public IAdminRepository repository;

        public AdminAuthenticateV1(IAdminRepository _repository)
        {
            repository = _repository;
        }

        public bool Exec(SqlConnection db, LoginInformation login, ref AuthenticatedUser loggedUser)
        {
            if (string.IsNullOrEmpty(login.Login))
            {
                Error = new ServiceError { Message = "Invalid login" };
                return false;
            }

            if (string.IsNullOrEmpty(login.Passwd))
            {
                Error = new ServiceError { Message = "Invalid password" };
                return false;
            }

            if (login.Passwd.Length < 6)
            {
                Error = new ServiceError { Message = "Invalid password" };
                return false;
            }

            var db_user = new Entities.Database.User();

            if (!repository.AdminLogin(db, login.Login, login.Passwd, ref db_user))
            {
                Error = new ServiceError { Message = "Invalid User Credentials" };
                return false;
            }

            loggedUser = new AuthenticatedUser
            {
                Id = db_user.Id,
                Name = db_user.Name,
                Email = db_user.Email,                
                Phone = db_user.Phone,
                Token = ""
            };

            return true;
        }
    }
}
