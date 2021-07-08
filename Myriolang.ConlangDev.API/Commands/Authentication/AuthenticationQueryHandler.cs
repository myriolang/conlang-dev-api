using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Myriolang.ConlangDev.API.Models;
using Myriolang.ConlangDev.API.Models.Responses;
using Myriolang.ConlangDev.API.Services;

namespace Myriolang.ConlangDev.API.Commands.Authentication
{
    public class AuthenticationQueryHandler : IRequestHandler<AuthenticationQuery, AuthenticationResponse>
    {
        private readonly IAuthService _authService;
        public AuthenticationQueryHandler(IAuthService authService) => _authService = authService;

        public async Task<AuthenticationResponse> Handle(AuthenticationQuery request,
            CancellationToken cancellationToken)
            => await _authService.Authenticate(request.Username, request.Password);
    }
}