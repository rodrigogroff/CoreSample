using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using RestSharp;

namespace Gateway.Controllers
{
    [ApiController]
    public class GatewayController : ControllerBase
    {
        public readonly LocalNetwork network;

        public GatewayController(IOptions<LocalNetwork> network)
        {
            this.network = network.Value;
        }

        [NonAction]
        public Method ConvertMethod(string methodStr)
        {
            switch (methodStr)
            {
                case "POST": return Method.POST;
                case "PUT": return Method.PUT;
            }

            return Method.GET;
        }

        [NonAction]
        public ActionResult<JsonResult> ExecutarServico(RestClient client, RestRequest request)
        {
            var response = client.Execute(request);

            switch (response.StatusCode)
            {
                default:
                case HttpStatusCode.BadRequest: return BadRequest(response.Content);

                case HttpStatusCode.OK: return Ok(response.Content);
            }
        }

        [NonAction]
        public void ObterSessao(ref RestRequest request )
        {
            request.AddHeader("SessionID", this.Request.Headers["SessionID"]);
        }
    }
}
