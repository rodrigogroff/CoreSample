using Api.Login.Controllers;
using Gateway.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Login.Domain
{
    public class ServiceLogin
    {
        public string error = "";

        public OutputLogin Validar(ReqLoginInformation login)
        {
            error = "Método não implementado";
            return null;
        }
    }
}
