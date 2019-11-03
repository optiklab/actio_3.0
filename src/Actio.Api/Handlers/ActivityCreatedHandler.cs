using System;
using System.Threading.Tasks;
using Actio.Api.Models;
using Actio.Api.Repositories;
using Actio.Common.Events;

namespace Actio.Api.Handlers
{
    public class ActivityCreatedHandler : IEventHandler<ActivityCreated>
    {
        private readonly IActivityRepository _activityRepository;

        public ActivityCreatedHandler(IActivityRepository activityRepository)
        {
            _activityRepository = activityRepository;
        }

        public async Task HandleAsync(ActivityCreated @event)
        {
            await _activityRepository.AddAsync(new Activity
            {
                // Flattened DTO model. It is used to do not pass whole object to Message Queue, but instead
                // save object in the microservice database. In this case, upon Get API call it can query object from
                // internal database.
                Id = @event.Id,
                Name = @event.Name,
                Category = @event.Category,
                Description = @event.Description,
                UserId = @event.UserId,
                CreatedAt = @event.CreatedAt
            });
            Console.WriteLine($"Activity created: {@event.Name}");
        }
    }
}