using Entities.Api.Configuration;
using Entities.Api.Portal;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using RestSharp;

namespace Integration
{
    public partial class Portal : BaseTest
    {
        [TestMethod]
        public void UserAuthenticate()
        {
            string email = "";

            CreateIntegrationUser(ref email);

            var client = new RestClient(master);
            var request = new RestRequest("api/v1/portal/authenticate", Method.POST);

            request.AddJsonBody(new LoginInformation
            {
                Login = email,
                Passwd = "123456",                
            });

            IRestResponse response = client.Execute(request);

            if (response.StatusCode != System.Net.HttpStatusCode.OK)
                Assert.Fail("login nok");

            var auth = JsonConvert.DeserializeObject<AuthenticatedUser>(response.Content);

            if (string.IsNullOrEmpty(auth.Token))
                Assert.Fail("auth.Token null");
        }
    }
}
