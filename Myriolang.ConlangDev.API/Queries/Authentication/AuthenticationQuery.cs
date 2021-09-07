using System.ComponentModel.DataAnnotations;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Myriolang.ConlangDev.API.Models;
using Myriolang.ConlangDev.API.Models.Responses;
using Myriolang.ConlangDev.API.Services;

namespace Myriolang.ConlangDev.API.Queries.Authentication
{
    public class AuthenticationQuery : IRequest<AuthenticationResponse>
    {
        [Required]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }
    }
    
    public class AuthenticationQueryHandler : IRequestHandler<AuthenticationQuery, AuthenticationResponse>
    {
        private readonly IAuthService _authService;
        public AuthenticationQueryHandler(IAuthService authService) => _authService = authService;

        public async Task<AuthenticationResponse> Handle(AuthenticationQuery request,
            CancellationToken cancellationToken)
            => await _authService.Authenticate(request.Username, request.Password);
    }
}