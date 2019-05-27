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
        public void EditSubCategory()
        {
            string email = "";

            string objCategName = "categ test " + GetRandomNumber(10000, 90000);
            string objSubCategName = "sub categ test " + GetRandomNumber(10000, 90000);

            var bearer = CreateAndAuthorizeAdmin(ref email);
            long categId = CreateProductCategory(bearer, objCategName);
            long subcategid = CreateProductSubCategory(bearer, categId, objSubCategName);

            var client = new RestClient(master);
            var request = new RestRequest("api/v1/admin/editsubcategory", Method.POST);

            request.AddHeader("Authorization", "Bearer " + bearer);

            request.AddJsonBody(new NewSubCategoryData
            {
                Id = subcategid,
                Name = objSubCategName + " edited"
            });

            IRestResponse response = client.Execute(request);

            if (response.StatusCode != System.Net.HttpStatusCode.OK)
                Assert.Fail("editsubcategory nok");

            using (var db = new SqlConnection(strCon))
            {
                var ret = db.QueryFirstOrDefault<Entities.Database.ProductCategory>(
                                "select * from ProductSubCategory where Id=" + subcategid +
                                " and ProductCategoryID=" + categId);

                if (ret == null)
                    Assert.Fail("editsubcategory nok 3");

                if (!ret.Name.EndsWith ("edited"))
                    Assert.Fail("editsubcategory nok 4");
            }            
        }
    }
}
