﻿using Entities.Api.Configuration;
using Entities.Api.Portal;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Api.Master.Controllers
{
    public partial class ConfigurationController : MasterController
    {
        [AllowAnonymous]
        [HttpPost("api/v1/admin/createAccount")]
        public ActionResult<string> Public_AdminCreateAccount([FromBody] NewUserData obj)
        {
            return ExecuteRemoteService(obj);
        }

        [AllowAnonymous]
        [HttpPost("api/v1/admin/authenticate")]
        public ActionResult<string> Public_AdminAuthenticate([FromBody] LoginInformation obj)
        {
            var resp = ExecuteRemoteService(obj);

            if (!this.IsOk)
                return resp;

            var auth = JsonConvert.DeserializeObject<AuthenticatedUser>(this.contentServiceResponse);
            var Token = ComposeTokenForSession(auth);

            return Ok(new { Token });
        }
    }
}
