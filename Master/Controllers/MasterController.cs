using System;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Text;
using Entities.Api.Configuration;
using Master;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using RestSharp;

namespace Api.Master.Controllers
{
    [ApiController]
    public class MasterController : ControllerBase
    {
        public const string AuthorizationTag = "Authorization";
        public string contentServiceResponse = "";
        public string currentCacheTag = "";

        public bool IsOk = false;

        public LocalNetwork network;
        public RestClient serviceClient;
        public RestRequest serviceRequest;
        public Features features;

        public LocalNetworkTypes myNetworkType;

        public MasterController(IOptions<Features> _feature, IOptions<LocalNetwork> _network)
        {
            this.features = _feature.Value;
            this.network = _network.Value;
        }

        [NonAction]
        public void SetupNetwork()
        {
            serviceClient = new RestClient(network.GetHost(myNetworkType));
            serviceRequest = new RestRequest(Request.Path.Value, ConvertMethod(Request.Method));
        }

        [NonAction]
        public void CacheGet(string tag)
        {
            currentCacheTag = tag;
            serviceClient = new RestClient(features.CacheLocation);
            serviceRequest = new RestRequest("api/memory/" + tag, Method.GET);
            ExecuteRemoteService(serviceClient, serviceRequest);
        }

        [NonAction]
        public void CacheUpdate()
        {
            serviceClient = new RestClient(features.CacheLocation);
            serviceRequest = new RestRequest("api/memorySave", Method.POST);

            serviceRequest.AddHeader("Content-Type", "application/json; charset=utf-8");
            serviceRequest.AddJsonBody(new
            {
                tag = currentCacheTag,
                cachedContent = contentServiceResponse
            });
            ExecuteRemoteService(serviceClient, serviceRequest);
        }

        [NonAction]
        public void SetupAuthenticatedNetwork()
        {
            SetupNetwork();
            SetAuthentication(ref serviceRequest);
        }

        [NonAction]
        public Method ConvertMethod( string methodStr)
        {
            switch (methodStr)
            {
                case "POST": return Method.POST;
                case "PUT": return Method.PUT;
            }

            return Method.GET;
        }

        [NonAction]
        public void SetAuthentication(ref RestRequest request)
        {
            request.AddHeader(AuthorizationTag, this.Request.Headers[AuthorizationTag]);
        }

        [NonAction]
        public string ComposeTokenForSession(AuthenticatedUser user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(LocalNetwork.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim("Id", user.Id.ToString()),
                    new Claim("Email", user.Email),
                    new Claim("Name", user.Name),
                }),
                Expires = DateTime.UtcNow.AddHours(3),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }

        [NonAction]
        public ActionResult<string> ExecuteRemoteService(RestClient client, RestRequest request)
        {
            IsOk = false;

            var response = client.Execute(request);
            var strResp = Cleanup(response.Content);

            switch (response.StatusCode)
            {
                default:
                case HttpStatusCode.BadRequest:
                    return BadRequest(strResp);

                case HttpStatusCode.OK:
                    IsOk = true;
                    contentServiceResponse = strResp;
                    return Ok(strResp);
            }
        }

        [NonAction]
        public string Cleanup(string src)
        {
            return src.Replace("\\\"", "\"").TrimStart('\"').TrimEnd('\"');
        }

        [NonAction]
        public string ReverseCachedContent(string src)
        {
            return src.Replace("<!>", "{").Replace (">!<", "}");
        }
    }
}
