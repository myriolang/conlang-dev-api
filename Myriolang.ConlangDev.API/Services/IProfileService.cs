using System;
using System.Threading;
using System.Threading.Tasks;
using Myriolang.ConlangDev.API.Commands.Profiles;
using Myriolang.ConlangDev.API.Models;
using Myriolang.ConlangDev.API.Models.Responses;

namespace Myriolang.ConlangDev.API.Services
{
    public interface IProfileService
    {
        public Task<Profile> FindById(string id, CancellationToken cancellationToken);
        public Task<Profile> FindByUsername(string username, CancellationToken cancellationToken);
        public Task<Profile> Create(CreateProfileCommand request, CancellationToken cancellationToken);
        public bool VerifyProfilePassword(Profile profile, string candidate);
        public Task<ValidationResponse> ValidateUsername(string username, CancellationToken cancellationToken);
        public Task<ValidationResponse> ValidateEmail(string email, CancellationToken cancellationToken);
    }
}