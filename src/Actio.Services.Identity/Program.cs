using Actio.Common.Commands;
using Actio.Common.Services;

namespace Actio.Services.Identity
{
    public class Program
    {
        public static void Main(string[] args)
        {
            ServiceHost.Create<Startup>(args)
                .UseRabbitMq()
                .SubscribeToCommand<CreateUser>() // Subscribe service to commands.
                .Build()
                .Run();
        }
    }
}
