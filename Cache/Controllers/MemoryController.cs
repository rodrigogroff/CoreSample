using Cache;
using Microsoft.AspNetCore.Mvc;

namespace ClusterApp.Controllers
{
    public class CacheRequest
    {
        public string tag { get; set; }
        public string cachedContent { get; set; }
    }

    [ApiController]
    public class MemoryController : ControllerBase
    {
        private ICacheManager _cache;

        public MemoryController(ICacheManager cache)
        {
            _cache = cache;
        }

        [HttpGet("api/memory/{tag}")]
        public ActionResult<string> Get(string tag)
        {
            var resp = _cache.GetAttr(tag);

            if (resp == null)
                return BadRequest();

            return Ok(resp);
        }

        [HttpPost("api/memorySave")]
        public void Post([FromBody] CacheRequest obj)
        {
            if (obj.cachedContent == "")
                _cache.CleanAttr(obj.tag);
            else
                _cache.SetAttr(obj.tag, obj.cachedContent);
        }
    }
}
