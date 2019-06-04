using RestSharp;
using System;

namespace PreCache
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

        public static int GetSubCat(int cat, ref int subcat)
        {
            subcat++;
            int totSubPerCat = 100;
            return (cat - 1) * totSubPerCat + subcat;
        }

        static void Main(string[] args)
        {
            Console.WriteLine("PreCaching");
            Console.WriteLine("---------------------------");

            int cat = 1, subcat = 0;

            while (true)
            {
                if (!NavigateProducts(cat, GetSubCat(cat, ref subcat)))
                    cat++;
            }
        }

        static bool NavigateProducts(int cat, int sub)
        {
            var client = new RestClient(master);

            int skip = GetNumber(0, 50) * 10;

            string requestStr = "api/v1/portal/products?categID=" + cat + "&subcategID=" + sub + "&skip=" + skip + "&take=10";

            Console.Write(requestStr);

            var request = new RestRequest(requestStr, Method.GET);

            IRestResponse response = client.Execute(request);

            return response.StatusCode == System.Net.HttpStatusCode.OK;
        }
    }
}
