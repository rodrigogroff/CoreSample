using Gateway.Controllers;

namespace Api.Usuario.Domain
{
    public class AuthenticateV1
    {
        public ServiceError Error = new ServiceError();
        public AuthenticatedUser loggedUser = new AuthenticatedUser();

        public bool authenticate(LoginInformation login)
        {
            loggedUser.Name = "Testezinho " + login.Login;
            return true;
        }
    }
}
