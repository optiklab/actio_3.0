using System;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;

namespace Actio.Common.Services
{
    public class ServiceHost : IServiceHost
    {
        private readonly IWebHost _webHost;

        public ServiceHost(IWebHost webHost)
        {
            _webHost = webHost;
        }

        public void Run() => _webHost.Run();

        public static HostBuilder Create<TStartup>(string[] args) where TStartup : class
        {
            Console.Title = typeof(TStartup).Namespace;

            // Prepare configuration data for the rest of the system with using IConfigurationBuilder
            var config = new ConfigurationBuilder()
                .AddEnvironmentVariables() // Load configuration from env variables (it's also used by default)
                .AddCommandLine(args) // Load configuration from command line arguments. For example, we can type Url and Port.
                //.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true); // we don't need it as we use default file names.
                .Build(); // It actually builds final configuration object.

            // TODO: Do we use Kestrel here or not? (since we didn't call .ConfigureWebHostDefaults)

            var webHostBuilder = WebHost.CreateDefaultBuilder(args)
                // https://docs.microsoft.com/en-us/aspnet/core/fundamentals/host/web-host?view=aspnetcore-3.0#host-configuration-values
                //.UseUrls("http://*:5000") https://docs.microsoft.com/en-us/aspnet/core/fundamentals/host/web-host?view=aspnetcore-3.0#override-configuration
                .UseConfiguration(config) // This ethod configures everything by reading appsettings.json instead of using AddConsole, AddDebug or AddEventLog
                .UseStartup<TStartup>();

            return new HostBuilder(webHostBuilder.Build());
        }
    }
}