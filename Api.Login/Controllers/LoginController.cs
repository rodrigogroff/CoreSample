using Microsoft.AspNetCore.Mvc;
using Gateway.Controllers;

namespace Api.Login.Controllers
{
    #region - data requests - 

    public class OutputLogin
    {
        public string SessionID { get; set; }

        public string Nome { get; set; }
    }

    #endregion 

    [ApiController]
    public class LoginController : ControllerBase
    {
        [HttpPost("api/login/autenticar")]
        public ActionResult<JsonResult> Post([FromBody] ReqLoginInformation login)
        {
            return BadRequest(new { error = "feio!!" });
        }

        /*
        // GET api/values
        [HttpGet("api/login/autorizar")]
        public ActionResult<IEnumerable<string>> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            return "value";
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }*/
    }
}
