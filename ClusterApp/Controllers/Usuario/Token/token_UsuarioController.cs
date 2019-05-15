using Microsoft.AspNetCore.Mvc;
using RestSharp;

namespace Gateway.Controllers
{
    public partial class UsuarioController : GatewayController
    {
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
