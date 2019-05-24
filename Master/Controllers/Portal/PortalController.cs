using Master;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Options;

namespace Api.Master.Controllers
{
    [Authorize]
    public partial class PortalController : MasterController
    {     
        public PortalController( IOptions<Features> _feature,
                                 IOptions<LocalNetwork> _network) :                             
                                 base (_feature, _network)
        {
            myNetworkType = LocalNetworkTypes.Portal;
        }
    }
}
