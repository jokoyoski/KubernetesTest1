using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KubernetesTestClusterIP.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace KubernetesTestClusterIP
{
    public class Startup
    {
        public Startup(IWebHostEnvironment env)
        {
            var builder = new ConfigurationBuilder();
            if (env.IsDevelopment())
            {
                builder.SetBasePath(env.ContentRootPath).AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
            }
            builder.AddEnvironmentVariables();
            Configuration = builder.Build();
            HostEnvironment = env;
        }


        public IConfigurationRoot Configuration { get; }
        public IWebHostEnvironment HostEnvironment { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<KubeTestUrl>(Configuration.GetSection("url"));
            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

           // app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
