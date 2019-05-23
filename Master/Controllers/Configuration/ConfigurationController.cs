using Master;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Options;

namespace Api.Master.Controllers
{
    [Authorize]
    public partial class ConfigurationController : MasterController
    {     
        public ConfigurationController( IOptions<Features> _feature,
                                        IOptions<LocalNetwork> _network) :                             
                                        base (_feature, _network)
        {
            myNetworkType = LocalNetworkTypes.Config;
        }
    }
}
