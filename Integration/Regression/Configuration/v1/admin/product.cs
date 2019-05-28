using Entities.Api.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using RestSharp;

namespace Integration
{
    public partial class Configuration : BaseTest
    {
        [TestMethod]
        public void Product()
        {
            string email = "";

            string objCategName = "categ test " + GetRandomNumber(10000, 90000);
            string objSubCategName = "sub categ test " + GetRandomNumber(10000, 90000);
            string prodName = "product " + GetRandomNumber(10000, 90000);
            
            var bearer = CreateAndAuthorizeAdmin(ref email);
            long categId = CreateProductCategory(bearer, objCategName);
            long subcategid = CreateProductSubCategory(bearer, categId, objSubCategName);
            long prodId = CreateProduct(bearer, prodName, categId, subcategid);

            var client = new RestClient(master);
            var request = new RestRequest("api/v1/admin/product/" + prodId, Method.GET);

            request.AddHeader("Authorization", "Bearer " + bearer);

            IRestResponse response = client.Execute(request);

            if (response.StatusCode != System.Net.HttpStatusCode.OK)
                Assert.Fail("product nok 1");

            var cat = JsonConvert.DeserializeObject<AdminProductData>(Cleanup(response.Content));

            if (cat.Name != prodName)
                Assert.Fail("product nok 2");

            if (cat.CategName != objCategName)
                Assert.Fail("product nok 3");

            if (cat.SubCategName != objSubCategName)
                Assert.Fail("product nok 4");

            if (cat.AdminCreation != email)
                Assert.Fail("product nok 5");

            if (cat.AdminEdit != email)
                Assert.Fail("product nok 6");
        }
    }
}
