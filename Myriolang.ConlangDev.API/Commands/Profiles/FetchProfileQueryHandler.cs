using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Myriolang.ConlangDev.API.Models;
using Myriolang.ConlangDev.API.Services;

namespace Myriolang.ConlangDev.API.Commands.Profiles
{
    public class FetchProfileQueryHandler : IRequestHandler<FetchProfileQuery, Profile>
    {
        private readonly IProfileService _profileService;
        public FetchProfileQueryHandler(IProfileService profileService) => _profileService = profileService;

        public async Task<Profile> Handle(FetchProfileQuery request, CancellationToken cancellationToken)
        {
            if (request.Id is not null && request.Id.Length > 0)
                return await _profileService.FindById(request.Id);
            if (request.Username is not null && request.Username.Length > 0)
                return await _profileService.FindByUsername(request.Username);
            return null;
        }
    }
}