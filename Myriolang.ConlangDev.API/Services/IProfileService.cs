using System;
using System.Threading.Tasks;
using Myriolang.ConlangDev.API.Commands.Profiles;
using Myriolang.ConlangDev.API.Models;
using Myriolang.ConlangDev.API.Models.Responses;

namespace Myriolang.ConlangDev.API.Services
{
    public interface IProfileService
    {
        public Task<Profile> FindById(string id);
        public Task<Profile> FindByUsername(string username);
        public Task<Profile> Create(NewProfileMutation request);
        public bool VerifyProfilePassword(Profile profile, string candidate);
        public Task<ValidationResponse> ValidateUsername(string username);
        public Task<ValidationResponse> ValidateEmail(string email);
    }
}