using Master.Controllers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using RestSharp;

namespace Integration
{
    public partial class Configuration : BaseTest
    {
        [TestMethod]
        public void Authenticate()
        {
            string email = "", clientGuid = "";

            CreateIntegrationUser(ref email, ref clientGuid);

            var client = new RestClient(master);
            var request = new RestRequest("api/v1/user/authenticate", Method.POST);

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
