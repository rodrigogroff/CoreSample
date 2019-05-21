using Api.User.Repository;
using Master.Controllers;

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

        public bool authenticate(LoginInformation login)
        {
            loggedUser = new AuthenticatedUser
            {
                Name = "Testezinho " + login.Login
            };

            return true;
        }
    }
}
