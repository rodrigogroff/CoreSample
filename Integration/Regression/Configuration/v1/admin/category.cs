using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using RestSharp;

namespace Integration
{
    public partial class Configuration : BaseTest
    {
        [TestMethod]
        public void AdminCategory()
        {
            string email = "";
            string objName = "categ test " + GetRandomNumber(10000, 90000);

            var bearer = CreateAndAuthorizeAdmin(ref email);
            var ret = CreateProductCategory(bearer, objName);

            var client = new RestClient(master);
            var request = new RestRequest("api/v1/admin/category/" + ret, Method.GET);

            request.AddHeader("Authorization", "Bearer " + bearer);

            IRestResponse response = client.Execute(request);

            if (response.StatusCode != System.Net.HttpStatusCode.OK)
                Assert.Fail("category nok 1");

            var cat = JsonConvert.DeserializeObject<Entities.Api.Configuration.AdminCategory>(Cleanup(response.Content));

            if (cat.Name != objName)
                Assert.Fail("category nok 2");
        }
    }
}
