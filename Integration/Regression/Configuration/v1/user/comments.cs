using Master.Controllers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using RestSharp;

namespace Integration
{
    public partial class Configuration : BaseTest
    {
        [TestMethod]
        public void Comments()
        {
            string email = "", client_guid = "";
            string bearer = CreateAndAuthorize(ref email, ref client_guid);

            var client = new RestClient(master);
            var request = new RestRequest("api/v1/user/comments", Method.GET);

            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Authorization", "Bearer " + bearer);

            IRestResponse response = client.Execute(request);

            if (response.StatusCode != System.Net.HttpStatusCode.OK)
                Assert.Fail("GetUser nok [1]");
        }

        /*
        [TestMethod]
        public void GetUser()
        {
            string email = "";
            string bearer = CreateAndAuthorize(ref email);

            var client = new RestClient(master);
            var request = new RestRequest("api/v1/user/3", Method.GET);

            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Authorization", "Bearer " + bearer);

            IRestResponse response = client.Execute(request);

            if (response.StatusCode != System.Net.HttpStatusCode.OK)
                Assert.Fail("GetUser nok [1]");

            
            var user = JsonConvert.DeserializeObject<AuthenticatedUser>(Cleanup(response.Content));

            if (user.Name != login)
                Assert.Fail("GetUser nok [2]");
                
        }*/
    }
}
