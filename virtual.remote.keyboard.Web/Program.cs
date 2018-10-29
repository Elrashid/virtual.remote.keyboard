using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace remote.keyboard.Web
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args).Build().Run();
        }


        // #Override configuration
        // [ASP.NET Core Web Host | Microsoft Docs] (https://docs.microsoft.com/en-us/aspnet/core/fundamentals/host/web-host?view=aspnetcore-2.1#override-configuration)
        public static IWebHostBuilder CreateWebHostBuilder(string[] args)
        {
            var config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("hostsettings.json", optional: true)
                .AddCommandLine(args)
                .Build();

            return WebHost.CreateDefaultBuilder(args)
                .UseUrls()
                .UseConfiguration(config)
                 .UseStartup<Startup>();
        }






        //public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
        //    WebHost.CreateDefaultBuilder(args)
        //        .UseStartup<Startup>();


        //[Enforce HTTPS in ASP.NET Core | Microsoft Docs](https://docs.microsoft.com/en-us/aspnet/core/security/enforcing-ssl?view=aspnetcore-2.1&tabs=visual-studio)
        //[Configuration in ASP.NET Core | Microsoft Docs](https://docs.microsoft.com/en-us/aspnet/core/fundamentals/configuration/?view=aspnetcore-2.1)

        //WebHost.CreateDefaultBuilder(args)
        //.ConfigureAppConfiguration((hostingContext, config) =>
        //{
        //    config.SetBasePath(Directory.GetCurrentDirectory());
        //    config.AddJsonFile("hostsettings.json", optional: true, reloadOnChange: true);
        //    config.AddCommandLine(args);
        //})
        // .UseUrls()
        //.UseStartup<Startup>();



    }
}
