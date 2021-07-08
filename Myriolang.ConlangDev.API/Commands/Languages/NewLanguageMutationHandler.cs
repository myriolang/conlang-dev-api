using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Myriolang.ConlangDev.API.Models;
using Myriolang.ConlangDev.API.Services;

namespace Myriolang.ConlangDev.API.Commands.Languages
{
    public class NewLanguageMutationHandler : IRequestHandler<NewLanguageMutation, Language>
    {
        private readonly ILanguageService _languageService;
        public NewLanguageMutationHandler(ILanguageService languageService) => _languageService = languageService;

        public Task<Language> Handle(NewLanguageMutation request, CancellationToken cancellationToken)
            => _languageService.Create(request);
    }
}