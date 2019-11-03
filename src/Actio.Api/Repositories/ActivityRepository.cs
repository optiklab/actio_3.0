using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Actio.Api.Models;
using MongoDB.Driver;
using MongoDB.Driver.Linq;

namespace Actio.Api.Repositories
{
    /// <summary>
    /// NOTE!!! It would be better to have some ApplicationService interface which is expose necessary actions with IActivityRepository.
    /// </summary>
    public class ActivityRepository : IActivityRepository
    {
        private readonly IMongoDatabase _database;
        public ActivityRepository(IMongoDatabase database)
        {
            _database = database;
        }

        public async Task AddAsync(Activity activity)
            => await Collection.InsertOneAsync(activity);

        public async Task<IEnumerable<Activity>> BrowseAsync(Guid userId)
            => await Collection
                .AsQueryable()
                .Where(x => x.UserId == userId)
                .ToListAsync();

        public async Task<Activity> GetAsync(Guid id)
            => await Collection
                .AsQueryable()
                .FirstOrDefaultAsync(x => x.Id == id); // <= MongoDB.Driver.Linq

        private IMongoCollection<Activity> Collection
            => _database.GetCollection<Activity>("Activities");
    }
}