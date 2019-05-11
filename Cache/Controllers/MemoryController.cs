using Cache;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;

namespace ClusterApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MemoryController : ControllerBase
    {
        private ICacheManager _cache;

        public MemoryController(ICacheManager cache)
        {
            _cache = cache;
        }

        // GET api/values
        [HttpGet]
        public ActionResult<List<string>> Get()
        {            
            return _cache.GetValues();            
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<string> Get(string id)
        {
            var resp = _cache.GetAttr(id);

            if (resp == null)
                _cache.SetAttr(id, "opa");

            return resp ?? "";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody] string tag, string value)
        {
            _cache.SetAttr(tag,value);
        }
    }
}
