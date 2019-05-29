using Entities.Api.Portal;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using RestSharp;

namespace Integration
{
    public partial class Portal : BaseTest
    {
        [TestMethod]
        public void ProductComment()
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

            {
                var client = new RestClient(master);
                var request = new RestRequest("api/v1/portal/productComment", Method.POST);

                request.AddHeader("Authorization", "Bearer " + bearer);

                request.AddJsonBody(new NewProductComment
                {
                    ProductID = prodId,
                    Comment = "hello"
                });

                IRestResponse response = client.Execute(request);

                if (response.StatusCode != System.Net.HttpStatusCode.OK)
                    Assert.Fail("ProductComment nok [1]");
            }

            {
                var client = new RestClient(master);
                var request = new RestRequest("api/v1/portal/product/" + prodId, Method.GET);

                IRestResponse response = client.Execute(request);

                var obj = JsonConvert.DeserializeObject<ProductDto>(Cleanup(response.Content));

                if (response.StatusCode != System.Net.HttpStatusCode.OK)
                    Assert.Fail("ProductComment nok [2]");

                if (obj.Comments.Count == 0)
                    Assert.Fail("ProductComment nok [3]");
            }
        }
    }
}
