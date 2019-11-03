using System;
using System.Threading.Tasks;
using Actio.Common.Events;

namespace Actio.Api.Handlers
{
    public class UserCreatedHandler : IEventHandler<UserCreated>
    {
        public async Task HandleAsync(UserCreated @event)
        {
            await Task.CompletedTask;
            Console.WriteLine($"User created: {@event.Name} {@event.Email}");
        }
    }
}