using Entities.Api.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RestSharp;

namespace Integration
{
    public partial class Configuration : BaseTest
    {
        [TestMethod]
        public void AdminCreateCategory()
        {
            string email = "";

            var bearer = CreateAndAuthorizeAdmin(ref email);

            var client = new RestClient(master);
            var request = new RestRequest("api/v1/admin/createcategory", Method.POST);

            request.AddHeader("Authorization", "Bearer " + bearer);

            request.AddJsonBody(new NewCategoryData
            {
                Name = "categ test"                
            });

            IRestResponse response = client.Execute(request);

            if (response.StatusCode != System.Net.HttpStatusCode.OK)
                Assert.Fail("login nok");
        }
    }
}
