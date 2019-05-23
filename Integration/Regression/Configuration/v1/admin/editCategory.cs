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
        public void EditCreateCategory()
        {
            string email = "";

            var bearer = CreateAndAuthorizeAdmin(ref email);

            {
                var client = new RestClient(master);
                var request = new RestRequest("api/v1/admin/createcategory", Method.POST);

                request.AddHeader("Authorization", "Bearer " + bearer);

                var r = new Random();
                Thread.Sleep(1);

                request.AddJsonBody(new NewCategoryData
                {
                    Name = "categ test" + r.Next(1, 10000).ToString()
                });

                IRestResponse response = client.Execute(request);

                if (response.StatusCode != System.Net.HttpStatusCode.OK)
                    Assert.Fail("createcategory nok");
            }

            {
                var client = new RestClient(master);
                var request = new RestRequest("api/v1/admin/editcategory", Method.POST);

                request.AddHeader("Authorization", "Bearer " + bearer);

                request.AddJsonBody(new NewCategoryData
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
