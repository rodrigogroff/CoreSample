using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Gateway
{
    public static class LocalNetworkTypes
    {
        public const string login = "login";
        public const string empresa = "empresa";
        public const string lojista = "lojista";
    }

    public class LocalNetwork
    {
        public List<string> logins { get; set; }
        public List<string> empresas { get; set; }
        public List<string> lojistas { get; set; }

        int idx_login = 0, idx_empresas = 0, idx_lojistas = 0;

        public string GetHost (string _type)
        {
            List<string> lst = null;
            int idx = 0;

            switch (_type)
            {
                case LocalNetworkTypes.login: lst = logins; idx = idx_login; break;
                case LocalNetworkTypes.empresa: lst = empresas; idx = idx_empresas; break;
                case LocalNetworkTypes.lojista: lst = lojistas; idx = idx_lojistas; break;
            }
            
            return ResolveHost(lst, ref idx);
        }

        string ResolveHost(List<string> lst, ref int idx)
        {
            if (lst == null) return null;
            if (lst.Count() == 0) return null;
            if (lst.Count() == 1) return logins[0];
            else
            {
                int max = lst.Count();

                if (++idx >= max)
                    idx = 0;

                return logins[idx];
            }
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
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
                app.UseDeveloperExceptionPage();

            app.UseMvc();
        }
    }
}
