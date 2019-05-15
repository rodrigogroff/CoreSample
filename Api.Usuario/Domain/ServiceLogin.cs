using Gateway.Controllers;

namespace Api.Usuario.Domain
{
    public class ServiceLogin
    {
        public string error = "";

        public bool Autenticar(LoginInformation login)
        {
            return true;
        }
    }
}
