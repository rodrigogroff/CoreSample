
namespace Gateway.Controllers
{
    public class ServiceError
    {
        public string Mensagem;
        public string Dados;
        public string DebugInfo;
    }

    public class LoginAuthentication
    {
        public string Token;
        public string Nome;
    }

    public class LoginInformation
    {
        public string Login;
        public string Passwd; 
    }
}
