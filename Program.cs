using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using MyAppAPI.Context;
using NLog.Web;

namespace MyAppApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var logger = NLogBuilder
            .ConfigureNLog("nLog.config")
            .GetCurrentClassLogger();

            var host = CreateHostBuilder(args).Build();

            using (var scope = host.Services.CreateScope())
            {
                try
                {
                    var context = scope.ServiceProvider.GetService<ContentContext>();

                    context.Database.EnsureDeleted(); // This will delete database on every start
                    context.Database.Migrate(); // remigrates database
                }
                catch (Exception ex)
                {
                    logger.Error(ex, "An error occured while migrating the database.");
                }
            }
            
            host.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>()
                    .UseNLog();
                });
    }
}
