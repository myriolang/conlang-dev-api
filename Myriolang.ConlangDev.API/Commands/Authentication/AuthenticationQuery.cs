using System.ComponentModel.DataAnnotations;
using MediatR;
using Myriolang.ConlangDev.API.Models;
using Myriolang.ConlangDev.API.Models.Responses;

namespace Myriolang.ConlangDev.API.Commands.Authentication
{
    public class AuthenticationQuery : IRequest<AuthenticationResponse>
    {
        [Required]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }
    }
}