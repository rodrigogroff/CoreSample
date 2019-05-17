using Microsoft.AspNetCore.Mvc;

namespace Gateway.Controllers
{
    public partial class UsuarioController : GatewayController
    {
        [HttpGet("api/v1/usuario/{id}")]
        public ActionResult<string> Get(int id)
        {
            SetupNetwork();
            GetAuthentication(ref serviceRequest);
            return ExecuteRemoteService(serviceClient, serviceRequest);
        }
    }
}
