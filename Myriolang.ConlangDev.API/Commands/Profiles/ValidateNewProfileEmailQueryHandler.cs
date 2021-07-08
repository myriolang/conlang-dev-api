using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Myriolang.ConlangDev.API.Models.Responses;
using Myriolang.ConlangDev.API.Services;

namespace Myriolang.ConlangDev.API.Commands.Profiles
{
    public class ValidateNewProfileEmailQueryHandler : IRequestHandler<ValidateNewProfileEmailQuery, ValidationResponse>
    {
        private readonly IProfileService _profileService;
        public ValidateNewProfileEmailQueryHandler(IProfileService profileService) => _profileService = profileService;
        
        public async Task<ValidationResponse> Handle(ValidateNewProfileEmailQuery request,
            CancellationToken cancellationToken) => await _profileService.ValidateEmail(request.Email);
    }
}