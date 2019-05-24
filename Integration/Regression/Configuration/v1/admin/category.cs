using Microsoft.VisualStudio.TestTools.UnitTesting;
using RestSharp;

namespace Integration
{
    public partial class Configuration : BaseTest
    {
        [TestMethod]
        public void AdminCategory()
        {
            string email = "";

            var bearer = CreateAndAuthorizeAdmin(ref email);
           
            var client = new RestClient(master);
            var request = new RestRequest("api/v1/admin/category/1", Method.GET);

            request.AddHeader("Authorization", "Bearer " + bearer);

            IRestResponse response = client.Execute(request);

            if (response.StatusCode != System.Net.HttpStatusCode.OK)
                Assert.Fail("categories nok");
        }
    }
}
