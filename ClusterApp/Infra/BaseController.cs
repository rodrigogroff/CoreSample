using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using RestSharp;

namespace Gateway.Controllers
{    
    public class BaseController : ControllerBase
    {
        protected string GetName()
        {
            var handler = new JwtSecurityTokenHandler();
            string authHeader = Request.Headers["Authorization"];
            authHeader = authHeader.Replace("Bearer ", "");
            var jsonToken = handler.ReadToken(authHeader);
            var tokenS = handler.ReadToken(authHeader) as JwtSecurityToken;

            var id = tokenS.Claims.First(claim => claim.Type == "unique_name")?.Value;

            return id;
        }
    }
}
