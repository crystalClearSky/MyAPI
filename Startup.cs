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

namespace MyAppApi
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers()
            .AddNewtonsoftJson();
            // services.AddMvc()
            // .AddNewtonsoftJson(options =>
            // {
            //     // Adds Camel Casing to all class objects. Individual classes can also have with JsonPropery() Attribute
            //     options.SerializerSettings.ContractResolver = new DefaultContractResolver();
            // });
            services.AddScoped<IGalleryData, GalleryData>();
            services.AddScoped<IAvatarData, AvatarData>();
            services.AddScoped<ICommentData, CommentData>();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseStatusCodePages();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
