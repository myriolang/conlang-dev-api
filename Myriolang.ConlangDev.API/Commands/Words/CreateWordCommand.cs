using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Myriolang.ConlangDev.API.Models;

namespace Myriolang.ConlangDev.API.Commands.Words
{
    public class CreateWordCommand : IRequest<Word>
    {
        [Required]
        public string LanguageId { get; set; }
        [Required]
        public string Headword { get; set; }
        public string Pronunciation { get; set; }
        public List<Sense> Senses { get; set; }
    }
    
    public class CreateWordCommandHandler : IRequestHandler<CreateWordCommand, Word>
    {
        public Task<Word> Handle(CreateWordCommand request, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }
    }
}