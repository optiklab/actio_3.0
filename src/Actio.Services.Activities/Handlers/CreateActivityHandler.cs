using System;
using System.Threading.Tasks;
using Actio.Common.Commands;
using Actio.Common.Events;
using Actio.Common.Exceptions;
using Actio.Services.Activities.Services;
using Microsoft.Extensions.Logging;
using RawRabbit;

namespace Actio.Services.Activities.Handlers
{
    public class CreateActivityHandler : ICommandHandler<CreateActivity>
    {
        private readonly IBusClient _busClient;
        private readonly IActivityService _activityService;
        private ILogger<CreateActivityHandler> _logger;

        public CreateActivityHandler(IBusClient busClient, IActivityService activityService, ILogger<CreateActivityHandler> logger)
        {
            _busClient = busClient;
            _activityService = activityService;
            _logger = logger;
        }

        public async Task HandleAsync(CreateActivity command)
        {
            _logger.LogInformation($"Creating Activity: {@command.Name}");

            try
            {
                await _activityService.AddAsync(command.Id, command.UserId, command.Category,
                    command.Name, command.Description, command.CreatedAt);
                // Fire event by putting event into the queue.
                await _busClient.PublishAsync(new ActivityCreated(command.Id, command.UserId, command.Category,
                    command.Name, command.Description));
            }
            catch (ActioException ex)
            {
                await _busClient.PublishAsync(new CreateActivityRejected(command.Id, ex.Code, ex.Message));
                _logger.LogError(ex.Message);
            }
            catch (Exception ex)
            {
                await _busClient.PublishAsync(new CreateActivityRejected(command.Id, "Unexpected error", ex.Message));
                _logger.LogError(ex.Message);
            }
        }
    }
}