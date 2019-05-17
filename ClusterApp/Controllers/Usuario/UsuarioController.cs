using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Options;

namespace Gateway.Controllers
{
    [Authorize]
    public partial class UsuarioController : GatewayController
    {     
        public UsuarioController(IOptions<LocalNetwork> network) : base (network)
        {
            myNetworkType = LocalNetworkTypes.Usuario;
        }
    }
}
