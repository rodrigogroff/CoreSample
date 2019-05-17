using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Options;

namespace Gateway.Controllers
{
    [Authorize]
    public partial class UserController : GatewayController
    {     
        public UserController(IOptions<LocalNetwork> network) : base (network)
        {
            myNetworkType = LocalNetworkTypes.User;
        }
    }
}
