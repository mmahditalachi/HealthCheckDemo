using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HealthCheckDemo.HealthChecks;
using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace HealthCheckDemo
{
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
            services.AddControllers();

            //just simple health check configuaration
            //services.AddHealthChecks()
            //    .AddCheck("Foo1 Service", () => HealthCheckResult.Healthy("check for services"));
            //if one of services is degraded applications services shows degraded
            //services.AddHealthChecks()
            //    .AddCheck("Foo1 Service", () => HealthCheckResult.Degraded("check for services"))
            //    .AddCheck("Foo2 Service", () => HealthCheckResult.Healthy("check for services"))
            //    .AddCheck("Foo3 Service", () => HealthCheckResult.Unhealthy("check for services"));

            // Adding tags to checks 
            services.AddHealthChecks()
                //adding response time health check
                .AddCheck<ResponseTimeHealthChecks>("Network speed test", null, new[] { "service" })
                .AddCheck("Foo1 Service", () => HealthCheckResult.Degraded("check for services"), new[] { "Tags" })
                .AddCheck("Foo2 Service", () => HealthCheckResult.Healthy("check for services"), new[] { "sql" })
                .AddCheck("Foo3 Service", () => HealthCheckResult.Unhealthy("check for services"));

            services.AddSingleton<ResponseTimeHealthChecks>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                // Adding health check endpoint

                //endpoints.MapHealthChecks("/health");
                endpoints.MapHealthChecks("/health", new HealthCheckOptions() 
                {  
                    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
                });

                // if predicate is false just show overal application health 
                // in fact it shows application is running or not !
                endpoints.MapHealthChecks("/quickhealth", new HealthCheckOptions()
                {
                    Predicate = _ => false
                });

                //filter checks with tags 
                endpoints.MapHealthChecks("/health/services", new HealthCheckOptions()
                {
                    Predicate = reg => reg.Tags.Contains("sql"),
                    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
                });

                endpoints.MapControllers();
            });
        }
    }
}
