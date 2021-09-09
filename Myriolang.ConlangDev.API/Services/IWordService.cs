using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Myriolang.ConlangDev.API.Commands.Words;
using Myriolang.ConlangDev.API.Models;

namespace Myriolang.ConlangDev.API.Services
{
    public interface IWordService
    {
        public Task<Word> FindById(string id, CancellationToken cancellationToken);
        public Task<IEnumerable<Word>> ListByLanguage(string languageId, CancellationToken cancellationToken);
        public Task<Word> Create(CreateWordCommand createWordCommand, CancellationToken cancellationToken);
    }
}