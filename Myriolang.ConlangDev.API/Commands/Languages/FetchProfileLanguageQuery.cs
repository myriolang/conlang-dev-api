using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using MediatR;
using Myriolang.ConlangDev.API.Models;

namespace Myriolang.ConlangDev.API.Commands.Languages
{
    public class FetchProfileLanguageQuery : IRequest<IEnumerable<Language>>
    {
        [Required]
        public string ProfileId { get; set; }
    }
}