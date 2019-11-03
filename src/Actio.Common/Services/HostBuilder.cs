using Microsoft.AspNetCore.Hosting;
using RawRabbit;

namespace Actio.Common.Services
{
    public class HostBuilder : BuilderBase
    {
        private readonly IWebHost _webHost;
        private IBusClient _bus;
        public HostBuilder(IWebHost webHost)
        {
            _webHost = webHost;
        }
        public BusBuilder UseRabbitMq()
        {
            _bus = (IBusClient)_webHost.Services.GetService(typeof(IBusClient));

            return new BusBuilder(_webHost, _bus);
        }
        public override ServiceHost Build()
        {
            return new ServiceHost(_webHost);
        }
    }
}