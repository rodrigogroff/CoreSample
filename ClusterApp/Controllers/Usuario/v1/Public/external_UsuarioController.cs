using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Gateway.Controllers
{
    public partial class UsuarioController : GatewayController
    {
        [AllowAnonymous]
        [HttpPost("api/v1/usuario/criarconta")]
        public ActionResult<string> CriarConta([FromBody] DadosNovaConta obj)
        {
            SetupNetwork();
            serviceRequest.AddJsonBody(obj);
            return ExecuteRemoteService(serviceClient, serviceRequest);
        }

        [AllowAnonymous]
        [HttpPost("api/v1/usuario/autenticar")]
        public ActionResult<string> Autenticar([FromBody] LoginInformation obj)
        {
            SetupNetwork();
            serviceRequest.AddJsonBody(obj);

            var resp = ExecuteRemoteService(serviceClient, serviceRequest);

            if (!this.IsOk)
                return resp;

            var auth = JsonConvert.DeserializeObject<UsuarioAutenticado>(this.contentServiceResponse);
            auth.Token = GeraToken(obj.Login);
            return Ok(auth);
        }        
    }
}
