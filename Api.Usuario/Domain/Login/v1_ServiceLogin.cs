using Gateway.Controllers;

namespace Api.Usuario.Domain
{
    public class ServiceLoginV1
    {
        public ServiceError Error = new ServiceError();
        public UsuarioAutenticado Auth = new UsuarioAutenticado();

        public bool Autenticar(LoginInformation login)
        {
            Auth.Nome = "Testezinho " + login.Login;
            return true;
        }
    }
}
