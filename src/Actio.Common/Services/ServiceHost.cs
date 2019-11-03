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
            var config = new ConfigurationBuilder()
                .AddEnvironmentVariables()
                .AddCommandLine(args) // We will use those arguments during Run our services via console. For example, we can type Url and Port.
                .Build();

            var webHostBuilder = WebHost.CreateDefaultBuilder(args)
                // https://docs.microsoft.com/en-us/aspnet/core/fundamentals/host/web-host?view=aspnetcore-2.2#capture-startup-errors
                .CaptureStartupErrors(true)
                // https://docs.microsoft.com/en-us/aspnet/core/fundamentals/host/web-host?view=aspnetcore-2.2#detailed-errors
                .UseSetting(WebHostDefaults.DetailedErrorsKey, "true")
                .UseConfiguration(config)
                .UseStartup<TStartup>();

            return new HostBuilder(webHostBuilder.Build());
        }
    }
}