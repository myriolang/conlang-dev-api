using System.ComponentModel.DataAnnotations;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Myriolang.ConlangDev.API.Models.Responses;
using Myriolang.ConlangDev.API.Services;

namespace Myriolang.ConlangDev.API.Queries.Languages
{
    public class ValidateNewLanguageSlugQuery : IRequest<ValidationResponse>
    {
        public string ProfileId { get; set; }
        [Required]
        public string Slug { get; set; }
    }
    
    public class ValidateNewLanguageSlugQueryHandler : IRequestHandler<ValidateNewLanguageSlugQuery, ValidationResponse>
    {
        private readonly ILanguageService _languageService;

        public ValidateNewLanguageSlugQueryHandler(ILanguageService languageService) =>
            _languageService = languageService;

        public async Task<ValidationResponse> Handle(ValidateNewLanguageSlugQuery request,
            CancellationToken cancellationToken) => await _languageService.ValidateSlug(request, cancellationToken);
    }
}