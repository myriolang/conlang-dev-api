using System.ComponentModel.DataAnnotations;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Myriolang.ConlangDev.API.Models;
using Myriolang.ConlangDev.API.Models.Responses;
using Myriolang.ConlangDev.API.Services;

namespace Myriolang.ConlangDev.API.Queries.Profiles
{
    public class ValidateNewProfileUsernameQuery : IRequest<ValidationResponse>
    {
        [Required]
        public string Username { get; set; }
    }
    
    public class ValidateNewProfileUsernameQueryHandler : IRequestHandler<ValidateNewProfileUsernameQuery, ValidationResponse>
    {
        private readonly IProfileService _profileService;

        public ValidateNewProfileUsernameQueryHandler(IProfileService profileService) =>
            _profileService = profileService;

        public async Task<ValidationResponse> Handle(ValidateNewProfileUsernameQuery validateNewProfileUsernameQuery,
            CancellationToken cancellationToken) => await _profileService
                .ValidateUsername(validateNewProfileUsernameQuery.Username, cancellationToken);
    }
}