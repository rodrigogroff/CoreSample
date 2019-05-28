using Dapper;
using Entities.Api.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using RestSharp;
using System.Data.SqlClient;

namespace Integration
{
    public partial class Configuration : BaseTest
    {
        [TestMethod]
        public void AdminCreateProduct()
        {
            string email = "";

            string objCategName = "categ test " + GetRandomNumber(10000, 90000);
            string objSubCategName = "sub categ test " + GetRandomNumber(10000, 90000);
            string objProductName = "product " + GetRandomNumber(10000, 90000);

            var bearer = CreateAndAuthorizeAdmin(ref email);
            long categId = CreateProductCategory (bearer, objCategName);
            long subcategid = CreateProductSubCategory(bearer, categId, objSubCategName);

            var client = new RestClient(master);
            var request = new RestRequest("api/v1/admin/createproduct", Method.POST);

            request.AddHeader("Authorization", "Bearer " + bearer);

            request.AddJsonBody(new NewProductData
            {
                Name = objProductName,
                ProductCategoryID = categId,
                ProductSubCategoryID = subcategid
            });

            IRestResponse response = client.Execute(request);

            if (response.StatusCode != System.Net.HttpStatusCode.OK)
                Assert.Fail("product nok");

            var obj = JsonConvert.DeserializeObject<CreateResp>(Cleanup(response.Content));

            if (obj.Id == 0)
                Assert.Fail("product nok");

            using (var db = new SqlConnection(strCon))
            {
                var ret = db.QueryFirstOrDefault<Entities.Database.Product>("select * from Product where Id=" + obj.Id);

                if (ret == null)
                    Assert.Fail("product nok 1");

                if (ret.Name != objProductName)
                    Assert.Fail("product nok 2");

                if (ret.ProductCategoryID != categId)
                    Assert.Fail("product nok 3");

                if (ret.ProductSubCategoryID != subcategid)
                    Assert.Fail("product nok 4");
            }
        }
    }
}
