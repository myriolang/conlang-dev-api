using System.Collections.Generic;
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

        public Task<Language> FindById(string id)
        {
            throw new System.NotImplementedException();
        }

        public Task<Language> FindBySlug(string slug)
        {
            throw new System.NotImplementedException();
        }

        public async Task<IEnumerable<Language>> FindByProfile(string profileId)
            => await _languages.Find(l => l.ProfileId == profileId).ToListAsync();

        public async Task<Language> Create(NewLanguageMutation mutation)
        {
            var language = Language.NewFromMutation(mutation);
            try
            {
                await _languages.InsertOneAsync(language);
                return language;
            }
            catch
            {
                return null;
            }
        }

        public async Task<ValidationResponse> ValidateSlug(ValidateNewLanguageSlugQuery query)
        {
            var count = await _languages.CountDocumentsAsync(
                l => l.ProfileId == query.ProfileId && l.Slug == query.Slug
            );
            var response = new ValidationResponse
            {
                Field = "slug",
                Value = query.Slug
            };
            response.Valid = count == 0;
            response.Message = count > 0 ? "Identifier already exists for that user" : null;
            return response;
        }
    }
}