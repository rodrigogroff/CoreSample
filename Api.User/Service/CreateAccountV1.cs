using Gateway.Controllers;
using System.Data.SqlClient;
using Api.User.Repository;

namespace Api.User.Service
{
    public class CreateAccountV1
    {
        public ServiceError Error;
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

            if (repository.UserExists (db, newUser.Email))
            {
                Error.Message = "User already registered";
                return false;
            }

            repository.AddUser(db, newUser);

            return true;
        }
    }
}
