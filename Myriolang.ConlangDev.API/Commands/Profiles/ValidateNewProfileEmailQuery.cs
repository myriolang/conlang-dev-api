using System.ComponentModel.DataAnnotations;
using MediatR;
using Myriolang.ConlangDev.API.Models.Responses;

namespace Myriolang.ConlangDev.API.Commands.Profiles
{
    public class ValidateNewProfileEmailQuery : IRequest<ValidationResponse>
    {
        [Required]
        public string Email { get; set; }
    }
}