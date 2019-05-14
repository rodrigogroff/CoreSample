using System.Net;
using Api.Login.Domain;
using Gateway.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using RestSharp;

namespace Api.Login.Controllers
{
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        [HttpPost("api/usuario/autenticar")]
        public ActionResult<JsonResult> Post([FromBody] ReqLoginInformation login)
        {
            var resp = new ServiceLogin().Autenticar(login);

            if (resp == null)
                return BadRequest(new { error = "feio!!" });
            else
                return Ok(resp);
        }

        [HttpGet("api/usuario/{id}")]
        public ActionResult<JsonResult> Get(int id)
        {
            return null;
        }
    }
}
