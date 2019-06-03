using Entities.Api.Configuration;
using Entities.Api.Portal;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Diagnostics;

namespace Crawler
{
    class Program
    {
        public static string master = "http://localhost:18523";
        public static string token = "http://localhost:18523";

        public static int GetNumber(int min, int max)
        {
            var r = new Random();

            return r.Next(min, max);
        }

        public static int GetRandomCat()
        {
            return GetNumber(1, 20);
        }

        public static int GetRandomSubCat(int cat)
        {
            int totSubPerCat = 100;
            return (cat - 1) * totSubPerCat + GetNumber(1, totSubPerCat);
        }

        static void Main(string[] args)
        {
            int myId = GetNumber(1, 100);

            Console.WriteLine("Login: " + myId);

            var client = new RestClient(master);
            var request = new RestRequest("api/v1/portal/authenticate", Method.POST);

            request.AddJsonBody(new LoginInformation
            {
                Login = "user"+ myId +"@client.com",
                Passwd = "123456",
            });

            IRestResponse response = client.Execute(request);

            if (response.StatusCode != System.Net.HttpStatusCode.OK)
            {
                Console.WriteLine("Login failed!");
            }
            else
            {
                token = JsonConvert.DeserializeObject<AuthenticatedUser>(response.Content).Token;

                while (true)
                {
                    int cat = GetRandomCat();
                    int sub = GetRandomSubCat(cat);

                    NavigateProducts(cat, sub);
                }
            }
        }

        static void NavigateProducts(int cat, int sub)
        {
            var sw = new Stopwatch();

            sw.Start();

            var client = new RestClient(master);

            int skip = GetNumber(0, 50) * 10;

            string requestStr = "api/v1/portal/products?categID=" + cat + "&subcategID=" + sub + "&skip=" + skip + "&take=10";

            Console.Write(requestStr);

            var request = new RestRequest(requestStr, Method.GET);
                        
            IRestResponse response = client.Execute(request);

            sw.Stop();

            Console.Write(" -> " + sw.ElapsedMilliseconds + "\n");

            if (response.StatusCode != System.Net.HttpStatusCode.OK)
            {
                Console.WriteLine("Login failed!");
                throw (new System.Exception("Failed!"));
            }
        }
    }
}
