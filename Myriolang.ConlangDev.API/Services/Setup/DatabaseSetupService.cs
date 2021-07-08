using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using MongoDB.Driver;
using Myriolang.ConlangDev.API.Models;

namespace Myriolang.ConlangDev.API.Services.Setup
{
    public class DatabaseSetupService : IHostedService
    {
        private readonly IMongoDatabase _database;
        
        public DatabaseSetupService(IConfiguration configuration)
        {
            var client = new MongoClient(configuration.GetSection("MongoDB")["ConnectionString"]);
            _database = client.GetDatabase(configuration.GetSection("MongoDB")["DatabaseName"]);
        }
        
        public async Task StartAsync(CancellationToken cancellationToken)
        {
            /*
             * unique index on profile username
             */
            var profiles = _database.GetCollection<Profile>("Profiles");
            var profileIndexDefinition = Builders<Profile>.IndexKeys
                .Ascending(p => p.Username);
            await profiles.Indexes.CreateOneAsync(
                new CreateIndexModel<Profile>(profileIndexDefinition, new CreateIndexOptions { Unique = true }),
                cancellationToken: cancellationToken
            );
            
            /*
             * unique compound index on language profile + slug
             */
            var languages = _database.GetCollection<Language>("Languages");
            var languageIndexDefinition = Builders<Language>.IndexKeys
                .Ascending(l => l.ProfileId)
                .Ascending(l => l.Slug);
            await languages.Indexes.CreateOneAsync(
                new CreateIndexModel<Language>(languageIndexDefinition, new CreateIndexOptions { Unique = true }),
                cancellationToken: cancellationToken
            );
        }

        public Task StopAsync(CancellationToken cancellationToken)
            => Task.CompletedTask;
    }
}