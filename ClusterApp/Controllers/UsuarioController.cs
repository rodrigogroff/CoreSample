using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using RestSharp;
using Newtonsoft.Json;

namespace Gateway.Controllers
{
    [Authorize]
    public class UsuarioController : GatewayController
    {
        public UsuarioController(IOptions<LocalNetwork> network) : base (network) { }

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
        
        [HttpGet("api/usuario/{id}")]
        public ActionResult<string> Get(int id)
        {
            var client = new RestClient(network.GetHost(LocalNetworkTypes.Usuario));
            var request = new RestRequest(Request.Path.Value, ConvertMethod(Request.Method));

            ObterDadosSessao(ref request);

            return ExecutarServico(client, request);
        }

        /*
        // GET api/values
        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {
            return new string[] { "value1", "value2" };
        }
        */
    }
}
