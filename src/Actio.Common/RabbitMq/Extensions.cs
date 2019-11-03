using System.Reflection;
using System.Threading.Tasks;
using Actio.Common.Commands;
using Actio.Common.Events;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RawRabbit;
using RawRabbit.Instantiation;

namespace Actio.Common.RabbitMq
{
    public static class Extensions
    {
        public static Task WithCommandHandlerAsync<TCommand>(this IBusClient bus, ICommandHandler<TCommand> handler) where TCommand : ICommand
            => bus.SubscribeAsync<TCommand>(msg => handler.HandleAsync(msg),
                ctx => ctx.UseSubscribeConfiguration(cfg =>
                    // Multiple instances of same service should subscribe to same single queue.
                    cfg.FromDeclaredQueue(q => q.WithName(GetQueueName<TCommand>()))));

        public static Task WithEventHandlerAsync<TEvent>(this IBusClient bus, IEventHandler<TEvent> handler) where TEvent : IEvent
            => bus.SubscribeAsync<TEvent>(msg => handler.HandleAsync(msg),
                ctx => ctx.UseSubscribeConfiguration(cfg =>
                    // Multiple instances of same service should subscribe to same single queue.
                    cfg.FromDeclaredQueue(q => q.WithName(GetQueueName<TEvent>()))));

        /// <summary>
        /// Resolves Queue name.
        /// Using slash in the name - it's usual (per author of Udemy).
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        private static string GetQueueName<T>()
            => $"{Assembly.GetEntryAssembly().GetName()}/{typeof(T).Name}";

        /// <summary>
        /// Resolve rabbitMQ client.
        /// </summary>
        /// <param name="services">Default IoC engine.</param>
        /// <param name="configuration"></param>
        public static void AddRabbitMq(this IServiceCollection services, IConfiguration configuration)
        {
            var options = new RabbitMqOptions();
            var section = configuration.GetSection("rabbitmq");
            section.Bind(options);

            var client = RawRabbitFactory.CreateSingleton(new RawRabbitOptions
            {
                ClientConfiguration = options
            });
            services.AddSingleton<IBusClient>(_ => client);
        }
    }
}