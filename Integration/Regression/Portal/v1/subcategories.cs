using Microsoft.VisualStudio.TestTools.UnitTesting;
using RestSharp;

namespace Integration
{
    public partial class Portal : BaseTest
    {
        [TestMethod]
        public void PortalSubCategories()
        {
            string email = "";

            var bearer = CreateAndAuthorizeAdmin(ref email);
            
            var client = new RestClient(master);
            var request = new RestRequest("api/v1/portal/subcategories?categid=1&skip=0&take=10", Method.GET);

            IRestResponse response = client.Execute(request);

            if (response.StatusCode != System.Net.HttpStatusCode.OK)
                Assert.Fail("subcategories nok");
        }
    }
}
