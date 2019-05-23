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
            var request = new RestRequest("api/v1/user/comments?skip=0&take=1", Method.GET);

            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Authorization", "Bearer " + bearer);

            IRestResponse response = client.Execute(request);

            if (response.StatusCode != System.Net.HttpStatusCode.OK)
                Assert.Fail("comments nok [1]");
        }
    }
}
