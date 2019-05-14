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
            }

            return Method.GET;
        }

        [NonAction]
        public ActionResult<JsonResult> ExecuteService(RestClient client, RestRequest request)
        {
            IRestResponse response = client.Execute(request);

            if (response.StatusCode != HttpStatusCode.OK)
                return BadRequest(response.Content);

            return Ok(response.Content);
        }
    }
}
