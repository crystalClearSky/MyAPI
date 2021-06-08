using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Net.Mime;
using System.Net.Security;
using System.Threading.Tasks;

using AutoMapper;
using ContentRepository;
using Entities.ContractsForDbContext;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.SqlServer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MyAppAPI.AppRepository;
using MyAppAPI.ContentRepository;
using MyAppAPI.Context;
using MyAppAPI.Entities.ContractsForDbContext;
using MyAppAPI.Services;
using Newtonsoft.Json.Serialization;

namespace MyAppApi
{
    public class Startup
    {
        private IWebHostEnvironment _env { get; set; }
        private IConfiguration Config { get; set; }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public Startup(IConfiguration config, IWebHostEnvironment enviroment)
        {
            _env = enviroment ?? throw new ArgumentNullException(nameof(enviroment));
            Config = config;
        }
        public void ConfigureServices(IServiceCollection services)
        {
            // if (!_env.IsDevelopment())
            // {
            //     services.AddHttpsRedirection( opt =>
            //     {
            //         opt.RedirectStatusCode = StatusCodes.Status308PermanentRedirect;
            //         opt.HttpsPort= 443;
            //     });
            // }

            services.AddControllers()
                .AddNewtonsoftJson(opt =>
                {
                    opt.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
                });

            // services.Configure<CookiePolicyOptions> (options => {
            //     // This lambda determines whether user consent for non-essential cookies is needed for a given request.
            //     options.CheckConsentNeeded = context => true;
            //     options.MinimumSameSitePolicy = SameSiteMode.None;
            // });

            services.AddDistributedMemoryCache();

            // services.AddDistributedSqlServerCache (o => {
            //     o.ConnectionString = "Server=;Database=ASPNET5SessionState;Trusted_Connection=True;";
            //     o.SchemaName = "dbo";
            //     o.TableName = "Sessions";
            // });

            services.AddHsts(options =>
            {
                options.Preload = true;
                options.IncludeSubDomains = true;
                options.MaxAge = TimeSpan.FromDays(60);
            });

            services.AddSession(opt =>
            {
                opt.Cookie.HttpOnly = true;
                opt.Cookie.SameSite = SameSiteMode.Strict;
                opt.Cookie.SecurePolicy = CookieSecurePolicy.Always;
                opt.Cookie.IsEssential = true;
                opt.IOTimeout = TimeSpan.FromMinutes(10); // session ends after 10m no matter what.
                opt.IdleTimeout = TimeSpan.FromMinutes(5); // session ends after 10s inactivity.
            });

            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies()); //typeof(Startup)

            services.AddCors();
            // services.AddMvc()
            // .AddNewtonsoftJson(options =>
            // {
            //     // Adds Camel Casing to all class objects. Individual classes can also have with JsonPropery() Attribute
            //     options.SerializerSettings.ContractResolver = new DefaultContractResolver();
            // });

            services.AddScoped<IGalleryData, GalleryData>();
            services.AddScoped<IAvatarData, AvatarData>();
            services.AddScoped<ICommentData, CommentData>();

            // var connectionString = @"Server=(localdb)\mssqllocaldb;Database=ContentContextDB;Trusted_Connection=True;";
            var connectionString = Config["connectionStrings:ContentConnectionString"];
            services.AddDbContext<ContentContext>(opt =>
            {
                opt.UseSqlServer(connectionString);
            });
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddScoped<IGalleryContext, GalleryDataRepo>();
            services.AddScoped<IAvatarContext, AvatarDataRepo>();
            services.AddScoped<IGuestContext, GuestDataRepo>();
            services.AddScoped<IAnonymousContext, AnonymousDataRepo>();
            services.AddScoped<IContactContext, ContactDataRepo>();
            services.AddScoped<CookieOptions>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();

            app.UseCookiePolicy();

            app.UseSession();

            app.UseRouting();

            app.UseCors(builder =>

               builder.WithOrigins("http://localhost:8080") //CHANGE BEFORE DEPLOYMENT!!
               .AllowAnyHeader()
               .AllowAnyMethod()
               .SetIsOriginAllowed(origin => true) // allow any origin
               .AllowCredentials()
            );

            app.UseStatusCodePages();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

        }

    }
}