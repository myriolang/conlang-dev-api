using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Myriolang.ConlangDev.API.Commands.Languages;
using Myriolang.ConlangDev.API.Models;
using Myriolang.ConlangDev.API.Models.Responses;
using Myriolang.ConlangDev.API.Queries.Languages;

namespace Myriolang.ConlangDev.API.Services
{
    public interface ILanguageService
    {
        public Task<Language> FindById(string id, CancellationToken cancellationToken);
        public Task<Language> FindBySlug(string slug, CancellationToken cancellationToken);
        public Task<IEnumerable<Language>> FindByProfile(string profileId, CancellationToken cancellationToken);
        public Task<Language> Create(CreateLanguageCommand createLanguageCommand, CancellationToken cancellationToken);
        public Task<ValidationResponse> ValidateSlug(ValidateNewLanguageSlugQuery validateNewLanguageSlugQuery,
            CancellationToken cancellationToken);
    }
}