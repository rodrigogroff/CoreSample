using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Master.Controllers
{
    public partial class UserController : MasterController
    {
        [AllowAnonymous]
        [HttpPost("api/v1/user/createAccount")]
        public ActionResult<string> createAccount([FromBody] NewUserData obj)
        {
            if (!this.features.CreateAccount.Execute)
                return BadRequest(new ServiceError
                {
                    Message = this.features.CreateAccount.ErrorMessage
                });

            SetupNetwork();
            serviceRequest.AddJsonBody(obj);
            return ExecuteRemoteService(serviceClient, serviceRequest);
        }

        [AllowAnonymous]
        [HttpPost("api/v1/user/authenticate")]
        public ActionResult<string> authenticate([FromBody] LoginInformation obj)
        {
            if (!this.features.Authenticate.Execute)
                return BadRequest(new ServiceError
                {
                    Message = this.features.Authenticate.ErrorMessage
                });

            SetupNetwork();
            serviceRequest.AddJsonBody(obj);
            
            var resp = ExecuteRemoteService(serviceClient, serviceRequest);

            if (!this.IsOk)
                return resp;

            var auth = JsonConvert.DeserializeObject<AuthenticatedUser>(this.contentServiceResponse);
            var Token = ComposeTokenForSession(auth);

            return Ok(new 
            {
                Token
            });
        }        
    }
}
