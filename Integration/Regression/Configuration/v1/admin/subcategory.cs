using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using RestSharp;

namespace Integration
{
    public partial class Configuration : BaseTest
    {
        [TestMethod]
        public void AdminSubCategory()
        {
            string email = "";
            string objCategName = "categ test " + GetRandomNumber(10000, 90000);
            string objSubCategName = "sub categ test " + GetRandomNumber(10000, 90000);

            var bearer = CreateAndAuthorizeAdmin(ref email);
            long categId = CreateProductCategory(bearer, objCategName);
            long subcategid = CreateProductSubCategory(bearer, categId, objSubCategName);

            var client = new RestClient(master);
            var request = new RestRequest("api/v1/admin/subcategory/" + subcategid, Method.GET);

            request.AddHeader("Authorization", "Bearer " + bearer);

            IRestResponse response = client.Execute(request);

            if (response.StatusCode != System.Net.HttpStatusCode.OK)
                Assert.Fail("subcategory nok 1");

            var cat = JsonConvert.DeserializeObject<Entities.Api.Configuration.AdminSubCategory>(Cleanup(response.Content));

            if (cat.Name != objSubCategName)
                Assert.Fail("subcategory nok 2");
        }
    }
}
