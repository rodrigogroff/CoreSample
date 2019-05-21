using Dapper;
using Master.Controllers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Data.SqlClient;

namespace Integration
{
    [TestClass]
    public class BaseTest
    {
        public string master = "http://localhost:18523";
        public string strCon = "Data Source=DESKTOP-6JMR2NF;Initial Catalog=VortigoServicePlatform;Integrated Security=SSPI;";
        
        public void CreateIntegrationUser(ref string email, ref string clientGuid)
        {
            #region - code - 

            var dtStamp = DateTime.Now.ToString("ddMMyyyyHHmmss");

            email = dtStamp + "_z@z.com";
            
            using (var db = new SqlConnection(strCon))
            {
                clientGuid = db.QueryFirstOrDefault<string>
                                ("select Guid from [Client] (nolock) where Id=@Client", new
                                {
                                    Client = 1
                                });
            }
            
            var client = new RestClient(master);
            var request = new RestRequest("api/v1/user/createAccount", Method.POST);

            request.AddJsonBody(new NewUserData
            {
                Email = email,
                Name = dtStamp + "_integration",
                Password = "123456",
                Phone = "",
                ClientGUID = clientGuid
            });

            IRestResponse response = client.Execute(request);

            if (response.StatusCode != System.Net.HttpStatusCode.OK)
                Assert.Fail("createAccount nok 1");
                        
            #endregion
        }

        public string CreateAndAuthorize(ref string email, ref string clientGuid)
        {
            #region - code - 

            CreateIntegrationUser(ref email, ref clientGuid);

            var client = new RestClient(master);
            var request = new RestRequest("api/v1/user/authenticate", Method.POST);

            request.AddJsonBody(new LoginInformation
            {
                Login = email,
                Passwd = "123456",
                ClientGuid = clientGuid
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

        public string Cleanup(string src)
        {
            return src.Replace("\\\"", "\"").TrimStart('\"').TrimEnd('\"');
        }
    }
}
