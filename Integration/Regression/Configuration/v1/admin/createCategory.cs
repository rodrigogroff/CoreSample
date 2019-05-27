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
        public long CreateProductCategory( string bearer, string objName )
        {
            var client = new RestClient(master);
            var request = new RestRequest("api/v1/admin/createcategory", Method.POST);

            request.AddHeader("Authorization", "Bearer " + bearer);

            request.AddJsonBody(new NewCategoryData
            {
                Name = objName
            });

            IRestResponse response = client.Execute(request);

            if (response.StatusCode != System.Net.HttpStatusCode.OK)
                Assert.Fail("createcategory nok");

            var obj = JsonConvert.DeserializeObject<CreateResp>(Cleanup(response.Content));

            if (obj.Id == 0)
                Assert.Fail("createcategory nok");

            using (var db = new SqlConnection(strCon))
            {
                var ret = db.QueryFirstOrDefault<Entities.Database.ProductCategory>("select * from ProductCategory where Id=" + obj.Id);

                if (ret == null)
                    Assert.Fail("createcategory nok 1");

                if (ret.Name != objName)
                    Assert.Fail("createcategory nok 2");
            }

            return obj.Id;
        }

        [TestMethod]
        public void AdminCreateCategory()
        {
            string email = "";
            string objName = "categ test " + GetRandomNumber(10000, 90000);

            var bearer = CreateAndAuthorizeAdmin(ref email);
            var ret = CreateProductCategory(bearer, objName);
        }
    }
}
