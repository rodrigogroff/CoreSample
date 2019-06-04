using System;
using System.Collections.Generic;
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
        public void SetupNetwork(object obj = null)
        {
            serviceClient = new RestClient(network.GetHost(myNetworkType));
            serviceRequest = new RestRequest(Request.Path.Value, ConvertMethod(Request.Method));

            if (obj != null)
                serviceRequest.AddJsonBody(obj);
        }

        [NonAction]
        public bool CacheGet(string tag, CacheAutomaticRecycle recycleParam)
        {
            currentCacheTag = tag;

            if (network.hshLocalCache[tag] is CachedLocalObject localObj)
            {
                if (localObj != null)
                {
                    if (DateTime.Now < localObj.expires)
                    {
                        contentServiceResponse = localObj.cachedContent;
                        network.UpdateRequestStat(contentServiceResponse.Length, true, true);
                        return true;
                    }
                    else
                        network.hshLocalCache[tag] = null;
                }                
            }

            serviceClient = new RestClient(features.CacheLocation);
            serviceRequest = new RestRequest("api/memory/" + tag, Method.GET);

            var response = serviceClient.Execute(serviceRequest);

            switch (response.StatusCode)
            {
                default:
                case HttpStatusCode.BadRequest:
                    return false;

                case HttpStatusCode.OK:
                    contentServiceResponse = Cleanup(response.Content);
                    network.UpdateRequestStat(contentServiceResponse.Length, true, false);

                    if (recycleParam != CacheAutomaticRecycle.Critical)
                    {
                        localObj = new CachedLocalObject
                        {
                            cachedContent = contentServiceResponse
                        };

                        switch (recycleParam)
                        {
                            case CacheAutomaticRecycle.Highest: localObj.expires = DateTime.Now.AddSeconds(10); break;
                            case CacheAutomaticRecycle.High: localObj.expires = DateTime.Now.AddSeconds(30); break;
                            case CacheAutomaticRecycle.Normal: localObj.expires = DateTime.Now.AddMinutes(1); break;
                            case CacheAutomaticRecycle.Low: localObj.expires = DateTime.Now.AddMinutes(5); break;
                            case CacheAutomaticRecycle.Lowest: localObj.expires = DateTime.Now.AddMinutes(10); break;
                        }

                        network.hshLocalCache[tag] = localObj;
                    }

                    return true;
            }
        }

        [NonAction]
        public string GetCacheMask_Products(long categID, long subcategId, int skip = 0, int take = 0)
        {
            if (skip == 0 && take == 0)
                return "PortalProducts_" + categID + "_" + subcategId + "_";
            else
                return "PortalProducts_" + categID + "_" + subcategId + "_" + skip + "_" + take + "_";
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

            ExecuteRemoteServiceCache(serviceClient, serviceRequest);
        }

        [NonAction]
        public void CacheClean(string tag)
        {
            serviceClient = new RestClient(features.CacheLocation);
            serviceRequest = new RestRequest("api/memorySave", Method.POST);

            serviceRequest.AddHeader("Content-Type", "application/json; charset=utf-8");
            serviceRequest.AddJsonBody(new
            {
                tag,
                cachedContent = ""
            });

            ExecuteRemoteServiceCache(serviceClient, serviceRequest);
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
        public ActionResult<string> ExecuteRemoteService (  object obj,                                                             
                                                            bool token = false, 
                                                            bool updateCache = true, 
                                                            List<string> lstCacheCleanup = null )
        {
            if (obj != null)
                SetupNetwork(obj);

            if (token == true)
                SetAuthentication(ref serviceRequest);

            IsOk = false;

            var response = serviceClient.Execute(serviceRequest);
            var strResp = Cleanup(response.Content);
            
            network.UpdateRequestStat(strResp.Length, false, false);

            switch (response.StatusCode)
            {
                default:
                case HttpStatusCode.BadRequest:
                    return BadRequest(strResp);

                case HttpStatusCode.OK:
                    IsOk = true;
                    contentServiceResponse = strResp;

                    if (features.Cache && updateCache == true)
                        CacheUpdate();

                    if (features.Cache && lstCacheCleanup != null)
                        foreach (var item in lstCacheCleanup)
                            CacheClean(item);

                    return Ok(strResp);
            }
        }

        [NonAction]
        public ActionResult<string> ExecuteRemoteServiceCache(RestClient client, RestRequest request)
        {
            var response = client.Execute(request);
            var strResp = Cleanup(response.Content);

            switch (response.StatusCode)
            {
                default:
                case HttpStatusCode.BadRequest:
                    return BadRequest(strResp);

                case HttpStatusCode.OK:
                    return Ok(strResp);
            }
        }

        [NonAction]
        public string Cleanup(string src)
        {
            return src.Replace("\\\"", "\"").TrimStart('\"').TrimEnd('\"');
        }
    }
}
