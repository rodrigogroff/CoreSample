using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;

namespace Api.Configuration
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseUrls("http://localhost:58564")
                .UseStartup<Startup>();
    }
}
