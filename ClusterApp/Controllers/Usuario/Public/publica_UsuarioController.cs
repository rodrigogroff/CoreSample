using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RestSharp;
using Newtonsoft.Json;

namespace Gateway.Controllers
{
    public partial class UsuarioController : GatewayController
    {
        [AllowAnonymous]
        [HttpPost("api/usuario/autenticar")]
        public ActionResult<string> Post([FromBody] LoginInformation obj)
        {
            var client = new RestClient(network.GetHost(LocalNetworkTypes.Usuario));
            var request = new RestRequest(Request.Path.Value, ConvertMethod(Request.Method));

            request.AddJsonBody(obj);

            var resp = ExecutarServico(client, request);

            if (this.IsOk)
            {
                var auth = JsonConvert.DeserializeObject<LoginAuthentication>(this.contentServiceResponse);

                auth.Token = GeraToken(obj.Login);

                return Ok(auth);
            }

            return resp;
        }
    }
}
