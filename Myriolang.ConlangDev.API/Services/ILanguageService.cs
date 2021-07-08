using System.Collections.Generic;
using System.Threading.Tasks;
using Myriolang.ConlangDev.API.Commands.Languages;
using Myriolang.ConlangDev.API.Models;

namespace Myriolang.ConlangDev.API.Services
{
    public interface ILanguageService
    {
        public Task<Language> FindById(string id);
        public Task<Language> FindBySlug(string slug);
        public Task<IEnumerable<Language>> FindByProfile(string profileId);
        public Task<Language> Create(NewLanguageMutation mutation);
    }
}