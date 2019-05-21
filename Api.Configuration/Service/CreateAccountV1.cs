using Master.Controllers;
using System.Data.SqlClient;
using Api.User.Repository;

namespace Api.User.Service
{
    public class CreateAccountV1
    {
        public ServiceError Error = new ServiceError();
        public IUserRepository repository;        

        public CreateAccountV1 (IUserRepository _repository)
        {
            repository = _repository;
        }

        public bool CreateAccount(SqlConnection db, NewUserData newUser)
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
            else
            {
                if (!newUser.Email.Contains("@"))
                {
                    Error.Message = "Email is invalid!";
                    return false;
                }
                else
                {
                    if (!newUser.Email.Split('@')[1].Contains("."))
                    {
                        Error.Message = "Email is invalid!";
                        return false;
                    }
                }
            }

            if (string.IsNullOrEmpty(newUser.Password))
            {
                Error.Message = "Password is empty!";
                return false;
            }
            
            if (newUser.Password.Length < 6)
            {
                Error.Message = "Password must be 6 characters at least";
                return false;
            }

            if (string.IsNullOrEmpty(newUser.ClientGUID))
            {
                Error.Message = "ClientID must be valid";
                return false;
            }

            if (!repository.ClientExists(db, newUser.ClientGUID))
            {
                Error.Message = "Client is invalid";
                return false;
            }

            if (repository.UserExists (db, newUser.Email, newUser.ClientGUID))
            {
                Error.Message = "User already registered";
                return false;
            }

            repository.AddUser(db, newUser);

            return true;
        }
    }
}
