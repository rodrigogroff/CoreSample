using Gateway.Controllers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using RestSharp;

namespace Integration.Gateway
{
    [TestClass]
    public class UserAPI : BaseTest
    {
        [TestMethod]
        public void Authenticate()
        {
            var client = new RestClient(Gateway);
            var request = new RestRequest("api/v1/user/authenticate", Method.POST);

            request.AddJsonBody(new LoginInformation
            {
                Login = "x",
                Passwd = "123"
            });

            IRestResponse response = client.Execute(request);

            if (response.StatusCode != System.Net.HttpStatusCode.OK)
                Assert.Fail("login nok");

            var auth = JsonConvert.DeserializeObject<AuthenticatedUser>(response.Content);

            if (string.IsNullOrEmpty(auth.Token))
                Assert.Fail("auth.Token null");
        }

        [TestMethod]
        public void GetUser()
        {
            string login = "x";

            string bearer = GetBearer(login, "123");

            var client = new RestClient(Gateway);
            var request = new RestRequest("api/v1/user/3", Method.GET);

            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Authorization", "Bearer " + bearer);

            IRestResponse response = client.Execute(request);

            if (response.StatusCode != System.Net.HttpStatusCode.OK)
                Assert.Fail("GetUser nok [1]");

            var user = JsonConvert.DeserializeObject<AuthenticatedUser>(Cleanup(response.Content));

            if (user.Name != login)
                Assert.Fail("GetUser nok [2]");
        }
    }
}
