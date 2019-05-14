
namespace Gateway.Controllers
{
    public class LoginAuthentication
    {
        public string SessionID;
        public string Nome;
    }

    public class ReqLoginInformation
    {
        public string Login { get; set; }

        public string Passwd { get; set; }
    }
}
