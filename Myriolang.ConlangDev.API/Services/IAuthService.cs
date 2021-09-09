using System.Threading;
using System.Threading.Tasks;
using Myriolang.ConlangDev.API.Models;
using Myriolang.ConlangDev.API.Models.Responses;

namespace Myriolang.ConlangDev.API.Services
{
    public interface IAuthService
    {
        public Task<AuthenticationResponse> Authenticate(string username, string password,
            CancellationToken cancellationToken);
        public Task<Profile> ValidateToken(string jwt, CancellationToken cancellationToken);
    }
}