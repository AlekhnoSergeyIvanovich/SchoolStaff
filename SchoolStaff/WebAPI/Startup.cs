using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NJsonSchema;
using NSwag.AspNetCore;
using WebAPI.Controllers;

namespace WebAPI
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
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            services.AddSwagger();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseSwaggerUi3(
                new[]
                {
                    //typeof(APIRetController)//,
                    typeof(ApiBusinessController),
                    typeof(ValuesController)
                },
                settings =>
                {
                    settings.GeneratorSettings.DefaultPropertyNameHandling =
                        PropertyNameHandling.CamelCase;

                    settings.GeneratorSettings.IsAspNetCore = true;
                    settings.PostProcess = document =>
                    {
                        document.Produces.Add("application/xml");
                        document.Info.Title = "School Management API";
                        document.Info.Description = "An API for School Management system, providing some statistics on what data is currently available in the system";

                        document.Info.TermsOfService = "None";
                        document.Info.Contact = new NSwag.SwaggerContact
                        {
                            Name = "Shayne Boyer",
                            Email = string.Empty,
                            Url = "https://twitter.com/spboyer"
                        };
                        document.Info.License = new NSwag.SwaggerLicense
                        {
                            Name = "Use under LICX",
                            Url = "https://example.com/license"
                        };
                    };
                });
            app.UseHttpsRedirection();
            app.UseMvc();

            app.UseDefaultFiles();
            app.UseStaticFiles();
        }
    }
}
