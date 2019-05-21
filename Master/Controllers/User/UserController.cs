using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Options;

namespace Master.Controllers
{
    [Authorize]
    public partial class UserController : MasterController
    {     
        public UserController ( IOptions<Features> _feature,
                                IOptions<LocalNetwork> _network) :                             
                                base (_feature, _network)
        {
            myNetworkType = LocalNetworkTypes.Config;
        }
    }
}
