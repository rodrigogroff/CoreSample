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
        public void AdminCategories()
        {
            string email = "";

            var bearer = CreateAndAuthorizeAdmin(ref email);
           
            var client = new RestClient(master);
            var request = new RestRequest("api/v1/admin/categories?skip=0&take=10", Method.GET);

            request.AddHeader("Authorization", "Bearer " + bearer);

            var r = new Random();
            Thread.Sleep(1);

            IRestResponse response = client.Execute(request);

            if (response.StatusCode != System.Net.HttpStatusCode.OK)
                Assert.Fail("categories nok");
        }
    }
}
