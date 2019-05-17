﻿using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using Microsoft.AspNetCore.Mvc;

namespace Gateway.Controllers
{    
    public class BaseController : ControllerBase
    {
        protected AuthenticatedUser GetCurrentAuthenticatedUser()
        {
            var handler = new JwtSecurityTokenHandler();
            var authHeader = Request.Headers[GatewayController.AuthorizationTag].ToString().Replace("Bearer ", "");
            var jsonToken = handler.ReadToken(authHeader);
            var tokenS = handler.ReadToken(authHeader) as JwtSecurityToken;

            return new AuthenticatedUser
            {
                Id = tokenS.Claims.FirstOrDefault(claim => claim.Type == "Id")?.Value,
                Phone = tokenS.Claims.FirstOrDefault(claim => claim.Type == "Phone")?.Value,
                Email = tokenS.Claims.FirstOrDefault(claim => claim.Type == "Email")?.Value,
                Name = tokenS.Claims.FirstOrDefault(claim => claim.Type == "unique_name")?.Value,                
            };
        }
    }
}