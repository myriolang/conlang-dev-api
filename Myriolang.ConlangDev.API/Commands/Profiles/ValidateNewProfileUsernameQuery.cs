using System.ComponentModel.DataAnnotations;
using MediatR;
using Myriolang.ConlangDev.API.Models;
using Myriolang.ConlangDev.API.Models.Responses;

namespace Myriolang.ConlangDev.API.Commands.Profiles
{
    public class ValidateNewProfileUsernameQuery : IRequest<ValidationResponse>
    {
        [Required]
        public string Username { get; set; }
    }
}