using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RestSharp;
using Newtonsoft.Json;

namespace Gateway.Controllers
{
    public partial class UsuarioController : GatewayController
    {
        [AllowAnonymous]
        [HttpPost("api/v1/usuario/autenticar")]
        public ActionResult<string> Autenticar([FromBody] LoginInformation obj)
        {
            var client = new RestClient(network.GetHost(LocalNetworkTypes.Usuario));
            var request = new RestRequest(Request.Path.Value, ConvertMethod(Request.Method));

            request.AddJsonBody(obj);

            var resp = ExecutarServico(client, request);

            if (this.IsOk)
            {
                var auth = JsonConvert.DeserializeObject<UsuarioAutenticado>(this.contentServiceResponse);

                auth.Token = GeraToken(obj.Login);

                return Ok(auth);
            }

            return resp;
        }

        [AllowAnonymous]
        [HttpPost("api/v1/usuario/criarconta")]
        public ActionResult<string> CriarConta([FromBody] DadosNovaConta obj)
        {
            var client = new RestClient(network.GetHost(LocalNetworkTypes.Usuario));
            var request = new RestRequest(Request.Path.Value, ConvertMethod(Request.Method));

            request.AddJsonBody(obj);

            return ExecutarServico(client, request);
        }
    }
}
