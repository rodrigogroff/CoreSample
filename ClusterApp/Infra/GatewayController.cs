using System;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using RestSharp;

namespace Gateway.Controllers
{
    [ApiController]
    public class GatewayController : ControllerBase
    {
        public const string Authorization = "Authorization";

        public readonly LocalNetwork network;

        public bool IsOk = false;
        public string contentServiceResponse = "";

        public GatewayController(IOptions<LocalNetwork> network)
        {
            this.network = network.Value;
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
        public void CopyAuthentication(ref RestRequest request)
        {
            request.AddHeader(Authorization, this.Request.Headers[Authorization]);
        }

        [NonAction]
        public ActionResult<string> ExecutarServico(RestClient client, RestRequest request)
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
        public string GeraToken(string loginName)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(LocalNetwork.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                        new Claim(ClaimTypes.Name, loginName),
                        new Claim("custom", "value")
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }
    }
}
