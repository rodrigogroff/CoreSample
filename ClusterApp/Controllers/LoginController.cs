using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using RestSharp;

namespace Gateway.Controllers
{
    public class ReqLoginInformation
    {
        public string Login { get; set; }

        public string Passwd { get; set; }
    }

    public class LoginController : GatewayController
    {
        public LoginController(IOptions<LocalNetwork> network) : base (network) { }

        [HttpPost("api/login/autenticar")]
        public ActionResult<JsonResult> Post([FromBody] ReqLoginInformation obj)
        {
            var client = new RestClient(network.GetHost(LocalNetworkTypes.login));
            var request = new RestRequest(Request.Path.Value, ConvertMethod(Request.Method));

            request.AddJsonBody(obj);

            return ExecuteService(client, request);
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            return "value";
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
