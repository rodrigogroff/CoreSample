using Cache;
using Microsoft.AspNetCore.Mvc;

namespace ClusterApp.Controllers
{
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
        public void Post([FromBody] string tag, string cachedContent)
        {
            if (cachedContent == "")
                _cache.SetAttr(tag, null);
            else
                _cache.SetAttr(tag, cachedContent);
        }
    }
}
