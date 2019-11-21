using Microsoft.AspNetCore.Hosting;
using RawRabbit;
using Actio.Common.Commands;
using Actio.Common.Events;
using Actio.Common.RabbitMq;

namespace Actio.Common.Services
{
    /// <summary>
    /// Actually it just wraps both Web Host Builder and subscription logic for Events or Commands to be used by microservices.
    /// </summary>
    public class BusBuilder : BuilderBase
    {
        private readonly IWebHost _webHost;
        private IBusClient _bus;

        public BusBuilder(IWebHost webHost, IBusClient bus)
        {
            _webHost = webHost;
            _bus = bus;
        }

        public BusBuilder SubscribeToCommand<TCommand>() where TCommand : ICommand
        {
            var handler = (ICommandHandler<TCommand>)_webHost.Services
                .GetService(typeof(ICommandHandler<TCommand>));
            _bus.WithCommandHandlerAsync(handler);

            return this;
        }

        /// <summary>
        /// Service host tries to grab Event Handler from services collection (default ASP.NET Core DI controller,
        /// which we initialize in method ConfigureServices at Startup calass) and then subscribe it to Event.
        /// Eventially event handler will be invoked if it found. If it is not found exception will be thrown.
        /// </summary>
        /// <returns></returns>
        public BusBuilder SubscribeToEvent<TEvent>() where TEvent : IEvent
        {
            var handler = (IEventHandler<TEvent>)_webHost.Services
                .GetService(typeof(IEventHandler<TEvent>));
            _bus.WithEventHandlerAsync(handler);

            return this;
        }

        public override ServiceHost Build()
        {
            return new ServiceHost(_webHost);
        }
    }
}