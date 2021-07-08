using System.ComponentModel.DataAnnotations;
using MediatR;
using Myriolang.ConlangDev.API.Models;

namespace Myriolang.ConlangDev.API.Commands.Profiles
{
    public class NewProfileMutation : IRequest<Profile>
    {
        [Required]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public string Email { get; set; }
    }
}