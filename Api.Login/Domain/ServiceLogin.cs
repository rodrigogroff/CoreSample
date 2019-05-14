using Gateway.Controllers;

namespace Api.Usuario.Domain
{
    public class ServiceLogin
    {
        public string error = "";

        public LoginAuthentication Autenticar(LoginInformation login)
        {
            return new LoginAuthentication
            {
                Nome = "Ferdinando " + login.Login,
                SessionID = "123",                
            };

            //error = "Método não implementado";
            //return null;
        }
    }
}
