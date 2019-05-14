using Microsoft.AspNetCore.Mvc;
using Gateway.Controllers;
using Api.Login.Domain;

namespace Api.Login.Controllers
{
    public class OutputLogin
    {
        public string SessionID;
        public string Nome;
    }
}
