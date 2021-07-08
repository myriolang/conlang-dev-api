using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Myriolang.ConlangDev.API.Models;
using Myriolang.ConlangDev.API.Services;

namespace Myriolang.ConlangDev.API.Commands.Profiles
{
    public class NewProfileMutationHandler : IRequestHandler<NewProfileMutation, Profile>
    {
        private readonly IProfileService _profileService;
        public NewProfileMutationHandler(IProfileService profileService) => _profileService = profileService;

        public Task<Profile> Handle(NewProfileMutation request, CancellationToken cancellationToken)
            => _profileService.Create(request);
    }
}