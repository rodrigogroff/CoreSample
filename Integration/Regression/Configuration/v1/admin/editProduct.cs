using Dapper;
using Entities.Api.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RestSharp;
using System.Data.SqlClient;

namespace Integration
{
    public partial class Configuration : BaseTest
    {
        [TestMethod]
        public void EditProduct()
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
            var request = new RestRequest("api/v1/admin/editproduct", Method.POST);

            request.AddHeader("Authorization", "Bearer " + bearer);

            request.AddJsonBody(new NewProductData
            {
                Id = prodId,
                Name = objSubCategName + " edited"
            });

            IRestResponse response = client.Execute(request);

            if (response.StatusCode != System.Net.HttpStatusCode.OK)
                Assert.Fail("editproduct nok");

            using (var db = new SqlConnection(strCon))
            {
                var ret = db.QueryFirstOrDefault<Entities.Database.Product>("select * from Product where Id=" + prodId);

                if (ret == null)
                    Assert.Fail("editproduct nok 1");

                if (!ret.Name.EndsWith("edited"))
                    Assert.Fail("editproduct nok 2");
            }
        }
    }
}
