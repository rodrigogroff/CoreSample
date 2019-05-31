using System.Collections;
using System.Collections.Generic;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Cache
{
    public interface ICacheManager
    {
        List<string> GetValues();
        string GetAttr(string tag);
        void SetAttr(string tag, string value);
    }

    public class CacheManager : ICacheManager
    {
        public Hashtable hsh = new Hashtable();
        public List<string> tags = new List<string>();

        public List<string> GetValues()
        {
            var ret = new List<string>();

            foreach (var item in tags)
                ret.Add(item + " -> " + hsh[item] as string);

            return ret;
        }

        string ICacheManager.GetAttr(string tag)
        {
            if (hsh[tag] == null) return null;

            return hsh[tag] as string;
        }

        void ICacheManager.SetAttr(string tag, string value)
        {
            hsh[tag] = value;

            if (!tags.Contains(tag))
                tags.Add(tag);
        }
    }

    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;            
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
            services.AddSingleton<ICacheManager, CacheManager>();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
#if DEBUG
            if (env.IsDevelopment())
                app.UseDeveloperExceptionPage();
#endif

            app.UseMvc();
        }
    }   
}
