using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Myriolang.ConlangDev.API.Commands.Words;
using Myriolang.ConlangDev.API.Models;

namespace Myriolang.ConlangDev.API.Services
{
    public interface IWordService
    {
        public Task<Word> FindByProfileLanguageId(string username, string languageSlug, string id,
            CancellationToken cancellationToken);
        public Task<IEnumerable<Word>> ListByProfileLanguage(string username, string languageSlug,
            CancellationToken cancellationToken);
        public Task<Word> Create(CreateWordCommand createWordCommand, CancellationToken cancellationToken);
    }
}