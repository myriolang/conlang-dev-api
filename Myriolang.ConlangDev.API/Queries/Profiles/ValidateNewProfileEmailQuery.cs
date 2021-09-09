using System.ComponentModel.DataAnnotations;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Myriolang.ConlangDev.API.Models.Responses;
using Myriolang.ConlangDev.API.Services;

namespace Myriolang.ConlangDev.API.Queries.Profiles
{
    public class ValidateNewProfileEmailQuery : IRequest<ValidationResponse>
    {
        [Required]
        public string Email { get; set; }
    }
    
    public class ValidateNewProfileEmailQueryHandler : IRequestHandler<ValidateNewProfileEmailQuery, ValidationResponse>
    {
        private readonly IProfileService _profileService;
        public ValidateNewProfileEmailQueryHandler(IProfileService profileService) => _profileService = profileService;
        
        public async Task<ValidationResponse> Handle(ValidateNewProfileEmailQuery validateNewProfileEmailQuery,
            CancellationToken cancellationToken) => await _profileService
                .ValidateEmail(validateNewProfileEmailQuery.Email, cancellationToken);
    }
}