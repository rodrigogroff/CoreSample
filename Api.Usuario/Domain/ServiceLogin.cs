using Gateway.Controllers;

namespace Api.Usuario.Domain
{
    public class ServiceLogin
    {
        public ServiceError Error = new ServiceError();
        public LoginAuthentication Auth = new LoginAuthentication();

        public bool Autenticar(LoginInformation login)
        {
            Auth.Nome = "Testezinho " + login.Login;
            return true;
        }
    }
}
