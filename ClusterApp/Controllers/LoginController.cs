using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using RestSharp;

namespace Gateway.Controllers
{
     public class LoginController : GatewayController
    {
        public LoginController(IOptions<LocalNetwork> network) : base (network) { }

        [HttpPost("api/usuario/autenticar")]
        public ActionResult<JsonResult> Post([FromBody] ReqLoginInformation obj)
        {
            var client = new RestClient(network.GetHost(LocalNetworkTypes.usuario));
            var request = new RestRequest(Request.Path.Value, ConvertMethod(Request.Method));

            request.AddJsonBody(obj);

            return ExecuteService(client, request);
        }

        // GET api/values/5
        [HttpGet("api/usuario/{id}")]
        public ActionResult<JsonResult> Get(int id)
        {
            var client = new RestClient(network.GetHost(LocalNetworkTypes.usuario));
            var request = new RestRequest(Request.Path.Value, ConvertMethod(Request.Method));
            
            return ExecuteService(client, request);
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
