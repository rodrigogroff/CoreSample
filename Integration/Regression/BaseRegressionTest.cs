using Entities.Api.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Threading;

namespace Integration
{
    [TestClass]
    public partial class Configuration : BaseTest
    {

    }

    [TestClass]
    public partial class Portal : BaseTest
    {

    }

    public class CreateResp

    {
        public long Id { get; set; }
    }


    [TestClass]
    public class BaseTest
    {
        public string master = "http://localhost:18523";
        public string strCon = "Data Source=DESKTOP-6JMR2NF;Initial Catalog=VortigoServicePlatform;Integrated Security=SSPI;";

        public string Cleanup(string src)
        {
            return src.Replace("\\\"", "\"").TrimStart('\"').TrimEnd('\"');
        }

        public string GetRandomNumber(int min, int max)
        {
            var r = new Random();
            Thread.Sleep(1);
            return r.Next(min, max).ToString();
        }

        public void CreateIntegrationAdmin(ref string email)
        {
            #region - code - 

            var r = new Random();
            Thread.Sleep(1);

            var dtStamp = DateTime.Now.ToString("ddMMyyyyHHmmss") + "_" + r.Next(1, 9999);

            email = dtStamp + "_z@admin.com";

            var client = new RestClient(master);
            var request = new RestRequest("api/v1/admin/createAccount", Method.POST);

            request.AddJsonBody(new NewUserData
            {
                Email = email,
                Name = dtStamp + "_integration",
                Password = "123456",
                Phone = "",
            });

            IRestResponse response = client.Execute(request);

            if (response.StatusCode != System.Net.HttpStatusCode.OK)
                Assert.Fail("createAccount nok 1");

            #endregion
        }

        public string CreateAndAuthorizeAdmin(ref string email)
        {
            #region - code - 

            CreateIntegrationAdmin(ref email);

            var client = new RestClient(master);
            var request = new RestRequest("api/v1/admin/authenticate", Method.POST);

            request.AddJsonBody(new LoginInformation
            {
                Login = email,
                Passwd = "123456",
            });

            IRestResponse response = client.Execute(request);

            if (response.StatusCode != System.Net.HttpStatusCode.OK)
                Assert.Fail("login nok");

            var auth = JsonConvert.DeserializeObject<AuthenticatedUser>(response.Content);

            if (string.IsNullOrEmpty(auth.Token))
                Assert.Fail("auth.Token null");

            return auth.Token;

            #endregion
        }

        public void CreateIntegrationUser(ref string email)
        {
            #region - code - 

            var r = new Random();

            Thread.Sleep(1);
            
            var dtStamp = DateTime.Now.ToString("ddMMyyyyHHmmss") + "_" + r.Next(1, 9999);

            email = dtStamp + "_z@z.com";
            
            var client = new RestClient(master);
            var request = new RestRequest("api/v1/user/createAccount", Method.POST);

            request.AddJsonBody(new NewUserData
            {
                Email = email,
                Name = dtStamp + "_integration",
                Password = "123456",
                Phone = "",
            });

            IRestResponse response = client.Execute(request);

            if (response.StatusCode != System.Net.HttpStatusCode.OK)
                Assert.Fail("createAccount nok 1");
                        
            #endregion
        }

        public string CreateAndAuthorizeUser(ref string email)
        {
            #region - code - 

            CreateIntegrationUser(ref email);

            var client = new RestClient(master);
            var request = new RestRequest("api/v1/user/authenticate", Method.POST);

            request.AddJsonBody(new LoginInformation
            {
                Login = email,
                Passwd = "123456",
            });

            IRestResponse response = client.Execute(request);

            if (response.StatusCode != System.Net.HttpStatusCode.OK)
                Assert.Fail("login nok");

            var auth = JsonConvert.DeserializeObject<AuthenticatedUser>(response.Content);

            if (string.IsNullOrEmpty(auth.Token))
                Assert.Fail("auth.Token null");

            return auth.Token;

            #endregion
        }
    }
}
