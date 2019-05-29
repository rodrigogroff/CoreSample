using Microsoft.VisualStudio.TestTools.UnitTesting;
using RestSharp;

namespace Integration
{
    public partial class Portal : BaseTest
    {
        [TestMethod]
        public void Products()
        {
            string email = "";
            string bearer = CreateAndAuthorizeUser(ref email);

            var config = new Configuration();

            string objCategName = "categ test " + GetRandomNumber(10000, 90000);
            string objSubCategName = "sub categ test " + GetRandomNumber(10000, 90000);
            string prodName = "product " + GetRandomNumber(10000, 90000);

            long categId = config.CreateProductCategory(bearer, objCategName);
            long subcategid = config.CreateProductSubCategory(bearer, categId, objSubCategName);
            long prodId = config.CreateProduct(bearer, prodName, categId, subcategid);

            var client = new RestClient(master);
            var request = new RestRequest("api/v1/portal/products?categid=1&subcategID=1&skip=0&take=10", Method.GET);
                
            IRestResponse response = client.Execute(request);

            if (response.StatusCode != System.Net.HttpStatusCode.OK)
                Assert.Fail("product nok [1]");
        }
    }
}
