using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Myriolang.ConlangDev.API.Models;
using Myriolang.ConlangDev.API.Services;

namespace Myriolang.ConlangDev.API.Queries.Languages
{
    public class FetchProfileLanguageQuery : IRequest<IEnumerable<Language>>
    {
        [Required]
        public string ProfileId { get; set; }
    }
    
    public class FetchProfileLanguageQueryHandler : IRequestHandler<FetchProfileLanguageQuery, IEnumerable<Language>>
    {
        private readonly ILanguageService _languageService;
        public FetchProfileLanguageQueryHandler(ILanguageService languageService) => _languageService = languageService;

        public async Task<IEnumerable<Language>> Handle(FetchProfileLanguageQuery request,
            CancellationToken cancellationToken)
            => await _languageService.FindByProfile(request.ProfileId);
    }
}