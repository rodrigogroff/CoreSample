using Gateway.Controllers;

namespace Api.Usuario.Domain
{
    public class ServiceLoginV1
    {
        public ServiceError Error = new ServiceError();
        public UsuarioAutenticado UsuarioLogado = new UsuarioAutenticado();

        public bool Autenticar(LoginInformation login)
        {
            UsuarioLogado.Nome = "Testezinho " + login.Login;
            return true;
        }
    }
}
