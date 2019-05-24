using Entities.Api.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RestSharp;
using System;
using System.Threading;

namespace Integration
{
    public partial class Configuration : BaseTest
    {
        [TestMethod]
        public void EditSubCategory()
        {
            string email = "";

            var bearer = CreateAndAuthorizeAdmin(ref email);

            {
                var client = new RestClient(master);
                var request = new RestRequest("api/v1/admin/editsubcategory", Method.POST);

                request.AddHeader("Authorization", "Bearer " + bearer);

                request.AddJsonBody(new NewSubCategoryData
                {
                    Id = 1,
                    Name = "categ test"
                });

                IRestResponse response = client.Execute(request);

                if (response.StatusCode != System.Net.HttpStatusCode.OK)
                    Assert.Fail("editcategory nok");
            }
        }
    }
}
