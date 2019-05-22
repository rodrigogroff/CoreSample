using System;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using RestSharp;

namespace Master.Controllers
{
    [ApiController]
    public class MasterController : ControllerBase
    {
        public const string AuthorizationTag = "Authorization";
        public string contentServiceResponse = "";

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
        public void GetAuthentication(ref RestRequest request)
        {
            request.AddHeader(AuthorizationTag, this.Request.Headers[AuthorizationTag]);
        }

        [NonAction]
        public ActionResult<string> ExecuteRemoteService(RestClient client, RestRequest request)
        {
            IsOk = false;

            var response = client.Execute(request);

            switch (response.StatusCode)
            {
                default:
                case HttpStatusCode.BadRequest:
                    return BadRequest(response.Content);

                case HttpStatusCode.OK:
                    IsOk = true;
                    contentServiceResponse = response.Content;
                    return Ok(response.Content);
            }
        }
        
        [NonAction]
        public string ComposeTokenForSession(AuthenticatedUser au)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(LocalNetwork.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim("Id", au.Id.ToString()),
                    new Claim("ClientID", au.ClientID.ToString()),
                    new Claim("Phone", au.Phone),
                    new Claim("Email", au.Phone),
                    new Claim("Name", au.Phone),
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }
    }
}
