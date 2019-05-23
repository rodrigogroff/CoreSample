using Master.Controllers;
using System.Data.SqlClient;
using Api.Configuration.Repository;
using Entities.Api.Configuration;
using Entities.Api;

namespace Api.Configuration.Service
{
    public class UserCreateAccountV1
    {
        public ServiceError Error;
        public IUserRepository repository;        

        public UserCreateAccountV1 (IUserRepository _repository)
        {
            repository = _repository;
        }

        public bool CreateAccount(SqlConnection db, NewUserData newUser)
        {
            if (string.IsNullOrEmpty(newUser.Name))
            {
                Error = new ServiceError { Message = "Name is empty!" };
                return false;
            }

            if (string.IsNullOrEmpty(newUser.Email))
            {
                Error = new ServiceError { Message = "Email is empty!" };
                return false;
            }
            else
            {
                if (!newUser.Email.Contains("@"))
                {
                    Error = new ServiceError { Message = "Email is invalid!" };
                    return false;
                }
                else
                {
                    if (!newUser.Email.Split('@')[1].Contains("."))
                    {
                        Error = new ServiceError { Message = "Email is invalid!" };
                        return false;
                    }
                }
            }

            if (string.IsNullOrEmpty(newUser.Password))
            {
                Error = new ServiceError { Message = "Password is empty!" };
                return false;
            }
            
            if (newUser.Password.Length < 6)
            {
                Error = new ServiceError { Message = "Password must be 6 characters at least" };
                return false;
            }

            if (repository.UserExists (db, newUser.Email))
            {
                Error = new ServiceError { Message = "User already registered" };
                return false;
            }

            repository.AddUser(db, newUser);

            return true;
        }
    }
}
