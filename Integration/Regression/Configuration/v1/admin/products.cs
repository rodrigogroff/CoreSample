using Microsoft.VisualStudio.TestTools.UnitTesting;
using RestSharp;

namespace Integration
{
    public partial class Configuration : BaseTest
    {
        [TestMethod]
        public void AdminProducts()
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
            var request = new RestRequest("api/v1/admin/products?categID=" + categId + "&subcategid=" + subcategid + "&skip=0&take=10", Method.GET);

            request.AddHeader("Authorization", "Bearer " + bearer);

            IRestResponse response = client.Execute(request);

            if (response.StatusCode != System.Net.HttpStatusCode.OK)
                Assert.Fail("categories nok");
        }
    }
}
