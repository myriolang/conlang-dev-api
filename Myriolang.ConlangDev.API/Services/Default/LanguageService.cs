using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using Myriolang.ConlangDev.API.Commands.Languages;
using Myriolang.ConlangDev.API.Models;
using Myriolang.ConlangDev.API.Models.Responses;
using Myriolang.ConlangDev.API.Queries.Languages;

namespace Myriolang.ConlangDev.API.Services.Default
{
    public class LanguageService : ILanguageService
    {
        private readonly IMongoCollection<Language> _languages;

        public LanguageService(IConfiguration configuration)
        {
            var client = new MongoClient(configuration.GetSection("MongoDB")["ConnectionString"]);
            var database = client.GetDatabase(configuration.GetSection("MongoDB")["DatabaseName"]);
            _languages = database.GetCollection<Language>("Languages");
        }

        public async Task<Language> FindById(string id, CancellationToken cancellationToken)
            => await _languages
                .Find(l => l.Id == id)
                .FirstOrDefaultAsync(cancellationToken)
                .ConfigureAwait(false);

        public async Task<Language> FindBySlug(string slug, CancellationToken cancellationToken)
            => await _languages
                .Find(l => l.Slug == slug)
                .FirstOrDefaultAsync(cancellationToken)
                .ConfigureAwait(false);

        public async Task<IEnumerable<Language>> FindByProfile(string profileId, CancellationToken cancellationToken)
            => await _languages
                .Find(l => l.ProfileId == profileId)
                .ToListAsync(cancellationToken)
                .ConfigureAwait(false);

        public async Task<Language> Create(CreateLanguageCommand createLanguageCommand,
            CancellationToken cancellationToken)
        {
            var language = Language.NewFromMutation(createLanguageCommand);
            try
            {
                await _languages
                    .InsertOneAsync(language, cancellationToken: cancellationToken)
                    .ConfigureAwait(false);
                return language;
            }
            catch
            {
                return null;
            }
        }

        public async Task<ValidationResponse> ValidateSlug(ValidateNewLanguageSlugQuery validateNewLanguageSlugQuery,
            CancellationToken cancellationToken)
        {
            var count = await _languages
                .CountDocumentsAsync(l =>
                        l.ProfileId == validateNewLanguageSlugQuery.ProfileId
                        && l.Slug == validateNewLanguageSlugQuery.Slug,
                    cancellationToken: cancellationToken)
                .ConfigureAwait(false);
            var response = new ValidationResponse
            {
                Field = "slug",
                Value = validateNewLanguageSlugQuery.Slug
            };
            response.Valid = count == 0;
            response.Message = count > 0 ? "Identifier already exists for that user" : null;
            return response;
        }
    }
}