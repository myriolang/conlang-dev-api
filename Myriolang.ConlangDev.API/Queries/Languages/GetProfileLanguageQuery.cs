using System.ComponentModel.DataAnnotations;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Myriolang.ConlangDev.API.Models;
using Myriolang.ConlangDev.API.Services;

namespace Myriolang.ConlangDev.API.Queries.Languages
{
    public class GetProfileLanguageQuery : IRequest<Language>
    {
        [Required]
        public string Username { get; set; }
        [Required]
        public string LanguageSlug { get; set; }

        public GetProfileLanguageQuery(string username, string languageSlug)
        {
            Username = username;
            LanguageSlug = languageSlug;
        }
    }
    
    public class GetProfileLanguageQueryHandler : IRequestHandler<GetProfileLanguageQuery, Language>
    {
        private readonly ILanguageService _languageService;
        public GetProfileLanguageQueryHandler(ILanguageService languageService) => _languageService = languageService;

        public Task<Language> Handle(GetProfileLanguageQuery request, CancellationToken cancellationToken)
            => _languageService.FindByProfileSlug(request.Username, request.LanguageSlug, cancellationToken);
    }
}