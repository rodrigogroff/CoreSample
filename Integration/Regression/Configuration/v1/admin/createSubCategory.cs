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
        public long CreateProductSubCategory(string bearer, long categId, string name)
        {
            var client = new RestClient(master);
            var request = new RestRequest("api/v1/admin/createsubcategory", Method.POST);

            request.AddHeader("Authorization", "Bearer " + bearer);

            request.AddJsonBody(new NewSubCategoryData
            {
                ProductCategoryID = categId,
                Name = name
            });

            IRestResponse response = client.Execute(request);

            if (response.StatusCode != System.Net.HttpStatusCode.OK)
                Assert.Fail("createsubcategory nok 1");

            var obj = JsonConvert.DeserializeObject<CreateResp>(Cleanup(response.Content));

            if (obj.Id == 0)
                Assert.Fail("createsubcategory nok 2");

            long subcategId = obj.Id;

            using (var db = new SqlConnection(strCon))
            {
                var ret = db.QueryFirstOrDefault<Entities.Database.ProductCategory>(
                                "select * from ProductSubCategory where Id=" + subcategId +
                                " and ProductCategoryID=" + categId);

                if (ret == null)
                    Assert.Fail("createsubcategory nok 3");

                if (ret.Name != name)
                    Assert.Fail("createsubcategory nok 4");
            }

            return subcategId;
        }

        [TestMethod]
        public void AdminCreateSubCategory()
        {
            string email = "";

            string objCategName = "categ test " + GetRandomNumber(10000, 90000);
            string objSubCategName = "sub categ test " + GetRandomNumber(10000, 90000);

            var bearer = CreateAndAuthorizeAdmin(ref email);
            long categId = CreateProductCategory (bearer, objCategName);
            long subcategid = CreateProductSubCategory(bearer, categId, objSubCategName);
        }
    }
}
