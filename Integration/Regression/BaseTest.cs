using Master.Controllers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using RestSharp;
using System;

namespace Integration
{
    [TestClass]
    public class BaseTest
    {
        public string master = "http://localhost:18523";

        public string CreateIntegrationUser()
        {
            var dtStamp = DateTime.Now.ToString("ddMMyyyyHHmmss");
            var _email = dtStamp + "_z@z.com";

            #region - create user - 
            {
                var client = new RestClient(master);
                var request = new RestRequest("api/v1/user/createAccount", Method.POST);

                request.AddJsonBody(new NewUserData
                {
                    Email = _email,
                    Name = dtStamp + "_integration",
                    Password = "123456",
                    Phone = "",
                });

                IRestResponse response = client.Execute(request);

                if (response.StatusCode != System.Net.HttpStatusCode.OK)
                    Assert.Fail("createAccount nok 1");
            }
            #endregion

            return _email;
        }

        public string CreateAndAuthorize(ref string email)
        {
            email = CreateIntegrationUser();

            #region - code - 

            var client = new RestClient(master);
            var request = new RestRequest("api/v1/user/authenticate", Method.POST);

            request.AddJsonBody(new LoginInformation
            {
                Login = email,
                Passwd = "123456"
            });

            IRestResponse response = client.Execute(request);

            if (response.StatusCode != System.Net.HttpStatusCode.OK)
                Assert.Fail("login nok");

            var auth = JsonConvert.DeserializeObject<AuthenticatedUser>(response.Content);

            if (string.IsNullOrEmpty(auth.Token))
                Assert.Fail("auth.Token null");

            return auth.Token;

            #endregion
        }

        public string Cleanup(string src)
        {
            return src.Replace("\\\"", "\"").TrimStart('\"').TrimEnd('\"');
        }
    }
}
