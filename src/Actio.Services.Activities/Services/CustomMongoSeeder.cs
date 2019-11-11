using System.Collections.Generic;
using System.Threading.Tasks;
using Actio.Common.Mongo;
using Actio.Services.Activities.Domain.Repositories;
using MongoDB.Driver;
using System.Linq;
using Actio.Services.Activities.Domain.Models;
using Microsoft.Extensions.Logging;

namespace Actio.Services.Activities.Services
{
    /// <summary>
    /// Override default seeder with custom implementation.
    /// </summary>
    public class CustomMongoSeeder : MongoSeeder
    {
        private ILogger<CustomMongoSeeder> _logger;
        private readonly ICategoryRepository _categoryRepository;
        public CustomMongoSeeder(IMongoDatabase database, ICategoryRepository categoryRepository, ILogger<CustomMongoSeeder> logger) : base(database)
        {
            _categoryRepository = categoryRepository;
            _logger = logger;
        }

        protected override async Task CustomSeedAsync()
        {
            var categories = new List<string>
            {
                "work",
                "sport",
                "hobby"
            };

            _logger.LogInformation($"Seeding DB");

            await Task.WhenAll(categories.Select(x
                => _categoryRepository.AddAsync(new Category(x))));
        }
    }
}