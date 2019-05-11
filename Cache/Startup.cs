using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Cache
{
    public class LocalNetwork
    {
        public List<string> cluster { get; set; }
    }

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

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            services.Configure<LocalNetwork>(Configuration.GetSection("localNetwork"));
            services.AddSingleton<ICacheManager, CacheManager>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();
        }
    }   
}
