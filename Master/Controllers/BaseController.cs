﻿using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using Entities.Api.Configuration;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using RestSharp;

namespace Api.Master.Controllers
{    
    public class BaseController : ControllerBase
    {
        public IConfiguration configuration;
        public const string _defaultError = "Oops. Something happened";

        [NonAction]
        public string GetDBConnectionString()
        {
#if DEBUG
            return configuration.GetConnectionString("Dev");
#endif

#if RELEASE
            return _config.GetConnectionString("Production");
#endif
        }

        [NonAction]
        public void SaveCacheContent(string cacheLocation, string tag, string cachedContent)
        {
            var serviceClient = new RestClient(cacheLocation);
            var serviceRequest = new RestRequest("api/memorySave", Method.POST);

            serviceRequest.AddHeader("Content-type", "application/json");

            serviceRequest.AddJsonBody(new
            {
                tag,
                cachedContent = cachedContent.Replace ("{","<!>").Replace("}",">!<")
            });

            var resp = serviceClient.Execute(serviceRequest);
        }

        [NonAction]
        protected AuthenticatedUser GetCurrentAuthenticatedUser()
        {
            var handler = new JwtSecurityTokenHandler();
            var authHeader = Request.Headers[MasterController.AuthorizationTag].ToString().Replace("Bearer ", "");

            if (string.IsNullOrEmpty(authHeader))
                return null;

            var jsonToken = handler.ReadToken(authHeader);
            var tokenS = handler.ReadToken(authHeader) as JwtSecurityToken;

            return new AuthenticatedUser
            {
                Id = Convert.ToInt64(tokenS.Claims.FirstOrDefault(claim => claim.Type == "Id")?.Value),
                Phone = tokenS.Claims.FirstOrDefault(claim => claim.Type == "Phone")?.Value,
                Email = tokenS.Claims.FirstOrDefault(claim => claim.Type == "Email")?.Value,
                Name = tokenS.Claims.FirstOrDefault(claim => claim.Type == "Name")?.Value,
            };
        }
    }
}
