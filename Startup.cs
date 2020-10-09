using System.Collections.Immutable;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MyAppAPI.AppRepository;
using MyAppAPI.Services;
using Newtonsoft.Json.Serialization;
using MyAppAPI.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace MyAppApi
{
    public class Startup
    {
        private IConfiguration Config { get; set;}

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public Startup(IConfiguration config)
        {
            Config = config;
        }
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers()
            .AddNewtonsoftJson();

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
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseCors(builder =>
            
            builder.WithOrigins("https://localhost:5100")
            .AllowAnyHeader()
            .AllowAnyMethod()
            );

            app.UseStatusCodePages();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

    }
}
