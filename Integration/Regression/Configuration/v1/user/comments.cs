using Microsoft.VisualStudio.TestTools.UnitTesting;
using RestSharp;

namespace Integration
{
    public partial class Configuration : BaseTest
    {
        [TestMethod]
        public void UserComments()
        {
            string email = "";
            string bearer = CreateAndAuthorizeUser(ref email);

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
