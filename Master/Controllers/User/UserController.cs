﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Options;

namespace Gateway.Controllers
{
    [Authorize]
    public partial class UserController : GatewayController
    {     
        public UserController ( IOptions<Features> _feature,
                                IOptions<LocalNetwork> _network) :                             
                                base (_feature, _network)
        {
            myNetworkType = LocalNetworkTypes.User;
        }
    }
}
