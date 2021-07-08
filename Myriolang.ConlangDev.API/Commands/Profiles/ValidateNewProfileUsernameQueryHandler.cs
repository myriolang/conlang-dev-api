using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Myriolang.ConlangDev.API.Models;
using Myriolang.ConlangDev.API.Models.Responses;
using Myriolang.ConlangDev.API.Services;

namespace Myriolang.ConlangDev.API.Commands.Profiles
{
    public class ValidateNewProfileUsernameQueryHandler : IRequestHandler<ValidateNewProfileUsernameQuery, ValidationResponse>
    {
        private readonly IProfileService _profileService;

        public ValidateNewProfileUsernameQueryHandler(IProfileService profileService) =>
            _profileService = profileService;

        public async Task<ValidationResponse> Handle(ValidateNewProfileUsernameQuery request,
            CancellationToken cancellationToken) => await _profileService.ValidateUsername(request.Username);
    }
}