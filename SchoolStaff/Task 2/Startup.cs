using System;
using Business;
using Business.API;
using Business.Class;
using Business.EditReferenceTable;
using Business.FactoryMethod;
using Business.MainPhone;
using Business.MessagesRep;
using Business.MessagesRep.Validation;
using Business.Repositories;
using DAL;
using DAL.Entities;
using HTP.Labs.SchoolStaff.Utils.DependencyInjection;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NLog.Extensions.Logging;
using NLog.Web;
using Presentation.AJAX;
using Presentation.Controllers;
using Presentation.SignalR;
using Presentation.Middleware;

namespace Task_2
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
            services.AddSignalR();

            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("SchoolManagementDatabase")), ServiceLifetime.Scoped);

            //-----------------------------------------------------------------------------------------
            //Decorator
            services.AddScoped<IRepository<SchoolStaff>, Repository<SchoolStaff>>()
                .AddDecorator<IRepository<SchoolStaff>, SchoolStaffDecorator>();
            services.AddScoped<IRepository<Subject>, Repository<Subject>>();
            services.AddScoped<IRepository<Profession>, Repository<Profession>>()
                .AddDecorator<IRepository<Profession>, ProfessionDecorator>();
            //-----------------------------------------------------------------------------------------

            services.AddScoped<IRepository<SchoolStaffPhone>, Repository<SchoolStaffPhone>>();
            services.AddScoped<IRepository<SchoolStaffProfession>, Repository<SchoolStaffProfession>>();
            services.AddScoped<IRepository<SchoolStaffSubject>, Repository<SchoolStaffSubject>>();

            //-----------------------------------------------------------------------------------------
            services.AddTransient<IAppHub, AppHub>();
            //-----------------------------------------------------------------------------------------
            //Update reference table
            services
                .AddTransient<IDeleteSchoolStaffProfessionFromProfession, DeleteSchoolStaffProfessionFromProfession>();
            services
                .AddTransient<IDeleteSchoolStaffProfessionFromSchoolStaff, DeleteSchoolStaffProfessionFromSchoolStaff
                >();
            services.AddTransient<IDeleteSchoolStaffSubjectFromSchoolStaff, DeleteSchoolStaffSubjectFromSchoolStaff>();
            services.AddTransient<IDeleteSchoolStaffSubjectFromSubject, DeleteSchoolStaffSubjectFromSubject>();

            //-----------------------------------------------------------------------------------------
            services.AddTransient<ISchoolStaffDataAjax, SchoolStaffDataAjax>();
            services.AddTransient<IProfessionDataAjax, ProfessionDataAjax>();
            //-----------------------------------------------------------------------------------------
            //API
            services.AddTransient<IAPISchoolStaffRepozitory, APISchoolStaffRepozitory>();
            services.AddTransient<IAPIProfessionArrayRepozitory, APIProfessionArrayRepozitory>();
            services.AddTransient<IAPISchoolStaffListRepozitory, APISchoolStaffListRepozitory>();
            services.AddTransient<IAPISubjecArrayRepozitory, APISubjecArrayRepozitory>();
            //-----------------------------------------------------------------------------------------
            //Factory method
            services.AddScoped<ICreator, ConcreteCreator>();
            //-----------------------------------------------------------------------------------------
            //Messages
            services.AddScoped<ILinqMessages, LinqMessages>();
            services.AddScoped<ISentMessageToAjax, SentMessageToAjax>();
            services.AddScoped<IVerifyTextMessage, VerifyTextMessage>();
            //-----------------------------------------------------------------------------------------
            //Identity Log
            services.AddSingleton<ILoggerFactory, NLogLoggerFactory>();
            //-----------------------------------------------------------------------------------------
            services.AddScoped<ICheckedPhone, CheckedPhone>();
            
            //-----------------------------------------------------------------------------------------


            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services.AddMvc().AddXmlSerializerFormatters();
            services.AddMvc().AddXmlDataContractSerializerFormatters();

            services.AddMvc(config =>
                {
                    var policy = new AuthorizationPolicyBuilder()
                        .RequireAuthenticatedUser()
                        .Build();
                    config.Filters.Add(new AuthorizeFilter(policy));
                }).
                SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            services.AddDefaultIdentity<IdentityUser>()
                .AddEntityFrameworkStores<ApplicationDbContext>();

            services.ConfigureApplicationCookie(options =>
            {
                // Cookie settings
                options.Cookie.HttpOnly = true;
                options.ExpireTimeSpan = TimeSpan.FromMinutes(5);

                options.LoginPath = "/Identity/Account/Login";
                options.AccessDeniedPath = "/Identity/Account/AccessDenied";
                options.SlidingExpiration = true;
            });

            services.AddAuthentication()
                .AddMicrosoftAccount(microsoftOptions =>
                {
                    microsoftOptions.ClientId = "cfdfd1a8-459d-4e63-8409-fcb44d811524";
                    microsoftOptions.ClientSecret = "qpiFUCX99!#}blvzIDI382-";
                })
                //Similarly
                //.AddGoogle(googleOptions => { ... })
                //.AddTwitter(twitterOptions => { ... })
                //.AddFacebook(facebookOptions => { ... })
                ;

            /*
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info
                {
                    Version = "v1",
                    Title = "Test API",
                    Description = "ASP.NET Core Web API"
                });
                c.IncludeXmlComments(XmlDocFactory());
            });
            */
        }
        
        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            app.UseContextAsCustomMiddleware<ApplicationDbContext>();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseSignalR(routes =>
            {
                routes.MapHub<AppHub>("/serg/actgo");
            });

            /*
            app.UseSwaggerUi3(
                new[]
                {
                    typeof(APIRetController),
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
                */
            app.UseAuthentication();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=SchoolStaff}/{action=Index}/{id?}"
                    );
            });

            loggerFactory.AddNLog();
            env.ConfigureNLog("Nlog.config");
            app.AddNLogWeb();
        }
    }
}