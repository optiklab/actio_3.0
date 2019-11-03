using System;
using System.Threading.Tasks;
using Actio.Services.Activities.Domain.Models;

namespace Actio.Services.Activities.Domain.Repositories
{
    public interface IActivityRepository
    {
        Task<Activity> GetAsync(Guid id);
        Task AddAsync(Activity activity);
    }
}