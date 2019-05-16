using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace Gateway
{
    public enum LocalNetworkTypes
    {
        Usuario = 0,
        Empresa = 1,
        Lojista = 2,
    }

    public class LocalNetwork
    {
        public const string Secret = "ciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1bmlxdWVfbmFtZSI6IjEiLCJuYmYiOjE1NTc5Mjk4ODcsImV4cCI6MTU1fhdsjhfeuyrejhdfj73333";
        public List<string> usuarios { get; set; }
        public List<string> empresas { get; set; }
        public List<string> lojistas { get; set; }

        int idx_usuario = 0, idx_empresas = 0, idx_lojistas = 0;

        public string GetHost (LocalNetworkTypes _type)
        {
            List<string> lst = null;
            int idx = 0;

            switch (_type)
            {
                case LocalNetworkTypes.Usuario: lst = usuarios; idx = idx_usuario; break;
                case LocalNetworkTypes.Empresa: lst = empresas; idx = idx_empresas; break;
                case LocalNetworkTypes.Lojista: lst = lojistas; idx = idx_lojistas; break;
            }
            
            return ResolveHost(lst, ref idx);
        }

        string ResolveHost(List<string> lst, ref int idx)
        {
            if (lst == null) return null;
            if (lst.Count() == 0) return null;
            if (lst.Count() == 1) return lst[0];
            else
            {
                int max = lst.Count();

                if (++idx >= max)
                    idx = 0;

                return usuarios[idx];
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

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            services.Configure<LocalNetwork>(Configuration.GetSection("localNetwork"));

            var key = Encoding.ASCII.GetBytes(LocalNetwork.Secret);
            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
                app.UseDeveloperExceptionPage();

            app.UseCors(x => x
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader());

            app.UseAuthentication();

            app.UseMvc();
        }
    }
}
