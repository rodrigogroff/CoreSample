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
        public void EditCategory()
        {
            string email = "";

            string objCategName = "categ test " + GetRandomNumber(10000, 90000);
            
            var bearer = CreateAndAuthorizeAdmin(ref email);
            long categId = CreateProductCategory(bearer, objCategName);
                        
            var client = new RestClient(master);
            var request = new RestRequest("api/v1/admin/editcategory", Method.POST);

            request.AddHeader("Authorization", "Bearer " + bearer);

            request.AddJsonBody(new NewCategoryData
            {
                Id = categId,
                Name = objCategName + " edited"
            });

            IRestResponse response = client.Execute(request);

            if (response.StatusCode != System.Net.HttpStatusCode.OK)
                Assert.Fail("editcategory nok 1");

            using (var db = new SqlConnection(strCon))
            {
                var ret = db.QueryFirstOrDefault<Entities.Database.ProductCategory>("select * from ProductCategory where Id=" + categId);

                if (ret == null)
                    Assert.Fail("editcategory nok 2");

                if (!ret.Name.EndsWith ("edited"))
                    Assert.Fail("editcategory nok 3");
            }
        }
    }
}
