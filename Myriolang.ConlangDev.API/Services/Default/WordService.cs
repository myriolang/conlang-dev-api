using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using Myriolang.ConlangDev.API.Commands.Words;
using Myriolang.ConlangDev.API.Models;

namespace Myriolang.ConlangDev.API.Services.Default
{
    public class WordService : IWordService
    {
        private readonly IMongoCollection<Word> _words;
        private readonly ILanguageService _languageService;

        public WordService(IConfiguration configuration, ILanguageService languageService)
        {
            var client = new MongoClient(configuration.GetSection("MongoDB")["ConnectionString"]);
            var database = client.GetDatabase(configuration.GetSection("MongoDB")["DatabaseName"]);
            _words = database.GetCollection<Word>("Words");
            _languageService = languageService;
        }

        public async Task<Word> FindByProfileLanguageId(string username, string languageSlug, string id,
            CancellationToken cancellationToken)
        {
            var language = await _languageService
                .FindByProfileSlug(username, languageSlug, cancellationToken)
                .ConfigureAwait(false);
            if (language is null) return null;
            return await _words
                .Find(w => w.LanguageId == language.Id && w.Id == id)
                .FirstOrDefaultAsync(cancellationToken)
                .ConfigureAwait(false);
        }

        public async Task<IEnumerable<Word>> ListByProfileLanguage(string username, string languageSlug,
            CancellationToken cancellationToken)
        {
            var language = await _languageService
                .FindByProfileSlug(username, languageSlug, cancellationToken)
                .ConfigureAwait(false);
            if (language is null) return null;
            return await _words
                .Find(w => w.LanguageId == language.Id)
                .ToListAsync(cancellationToken)
                .ConfigureAwait(false);
        }

        public async Task<Word> Create(CreateWordCommand createWordCommand, CancellationToken cancellationToken)
        {
            var language = await _languageService
                .FindById(createWordCommand.LanguageId, cancellationToken)
                .ConfigureAwait(false);
            if (language is null) return null;
            var word = new Word
            {
                LanguageId = language.Id,
                Headword = createWordCommand.Headword,
                Pronunciation = createWordCommand.Pronunciation,
                Senses = createWordCommand.Senses,
                Created = DateTime.Now
            };
            try
            {
                await _words
                    .InsertOneAsync(word, cancellationToken: cancellationToken)
                    .ConfigureAwait(false);
                return word;
            }
            catch
            {
                return null;
            }
        }
    }
}