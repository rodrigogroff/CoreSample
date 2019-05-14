/*
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace Cache.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HostsController : ControllerBase
    {
        private readonly LocalNetwork _myConfiguration;

        public HostsController(IOptions<LocalNetwork> myConfiguration)
        {
            _myConfiguration = myConfiguration.Value;
        }

        [HttpGet]
        public ActionResult<List<string>> Get()
        {
            return Ok(_myConfiguration.cluster);
        }
    }
}
*/