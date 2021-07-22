using System.ComponentModel.DataAnnotations;
using MediatR;
using Myriolang.ConlangDev.API.Models.Responses;

namespace Myriolang.ConlangDev.API.Commands.Languages
{
    public class ValidateNewLanguageSlugQuery : IRequest<ValidationResponse>
    {
        public string ProfileId { get; set; }
        [Required]
        public string Slug { get; set; }
    }
}