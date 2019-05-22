using Api.User.Repository;
using Master.Controllers;
using System.Data.SqlClient;

namespace Api.User.Service
{
    public class AuthenticateV1
    {
        public ServiceError Error;
        public AuthenticatedUser loggedUser;
        public IUserRepository repository;

        public AuthenticateV1(IUserRepository _repository)
        {
            repository = _repository;
        }

        public bool authenticate(SqlConnection db, LoginInformation login)
        {
            if (string.IsNullOrEmpty(login.ClientGuid))
            {
                Error = new ServiceError { Message = "Invalid Client" };
                return false;
            }

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

            if (!repository.ClientExists(db, login.ClientGuid))
            {
                Error = new ServiceError { Message = "Invalid Client" };
                return false;
            }

            var db_user = new Database.User();

            if (!repository.UserLogin(db, login.Login, login.Passwd, login.ClientGuid, ref db_user))
            {
                Error = new ServiceError { Message = "Invalid User Credentials" };
                return false;
            }

            loggedUser = new AuthenticatedUser
            {
                Id = db_user.Id,
                ClientID = db_user.ClientID,
                Name = db_user.Name,
                Email = db_user.Email,                
                Phone = db_user.Phone,
                Token = ""
            };

            return true;
        }
    }
}
