using Actio.Common.Events;
using Actio.Common.Services;
using Microsoft.Extensions.Hosting;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;
//using Microsoft.AspNetCore.Hosting;
//using Microsoft.Extensions.Configuration;
//using Microsoft.Extensions.Hosting;
//using Microsoft.Extensions.Logging;

namespace Actio.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            //CreateHostBuilder(args).Build().Run();
            ServiceHost.Create<Startup>(args)
                .UseRabbitMq()
                .SubscribeToEvent<ActivityCreated>() // Subscribe API to events.
                .Build()
                .Run();
        }

        // NOTE: https://docs.microsoft.com/en-us/aspnet/core/fundamentals/host/generic-host?view=aspnetcore-3.0
        // NOTE: Do not change method CreateHostBuilder name or signature if you use EF Core 2: https://docs.microsoft.com/en-us/ef/core/miscellaneous/cli/dbcontext-creation
        public static IHostBuilder CreateHostBuilder(string[] args)
        {
        // For Web load:
        //    Host.CreateDefaultBuilder(args)
        //        .ConfigureWebHostDefaults(webBuilder =>
        //        {
        //            webBuilder.UseStartup<Startup>();
        //        });

        // The ConfigureWebHostDefaults method:
        //    Loads host configuration from environment variables prefixed with "ASPNETCORE_".
        //    Sets Kestrel server as the web server and configures it using the app's hosting configuration providers.
        //       For the Kestrel server's default options, see Kestrel web server implementation in ASP.NET Core.
        //       (https://docs.microsoft.com/en-us/aspnet/core/fundamentals/servers/kestrel?view=aspnetcore-3.0#kestrel-options)
        //       Sample: https://github.com/aspnet/AspNetCore.Docs/tree/master/aspnetcore/fundamentals/servers/kestrel/samples/3.x/KestrelSample
        //    Adds Host Filtering middleware. (https://docs.microsoft.com/en-us/aspnet/core/fundamentals/servers/kestrel?view=aspnetcore-3.0#host-filtering)
        //    Adds Forwarded Headers middleware if ASPNETCORE_FORWARDEDHEADERS_ENABLED=true. (https://docs.microsoft.com/en-us/aspnet/core/host-and-deploy/proxy-load-balancer?view=aspnetcore-3.0#forwarded-headers)
        //    Enables IIS integration. For the IIS default options, see Host ASP.NET Core on Windows with IIS. (https://docs.microsoft.com/en-us/aspnet/core/host-and-deploy/iis/index?view=aspnetcore-3.0#iis-options)

        //  OR for non-HTTP workload
        //    Host.CreateDefaultBuilder(args)
        //        .ConfigureServices((hostContext, services) =>
        //        {
        //            services.AddHostedService<MyWorker>();
        //        });

        // The CreateDefaultBuilder method:
        //    Sets the content root to the path returned by GetCurrentDirectory.
        //    Loads host configuration from:
        //        Environment variables prefixed with "DOTNET_".
        //        Command-line arguments.
        //    Loads app configuration from:
        //        appsettings.json.
        //        appsettings.{Environment}.json.
        //        Secret Manager when the app runs in the Development environment.
        //        Environment variables.
        //        Command-line arguments.
        //    Adds the following logging providers (https://docs.microsoft.com/en-us/aspnet/core/fundamentals/logging/index?view=aspnetcore-3.0):
        //        Console
        //        Debug
        //        EventSource
        //        EventLog (only when running on Windows)
        //    Enables scope validation and dependency validation when the environment is Development.
            return null;
        }
    }
}
