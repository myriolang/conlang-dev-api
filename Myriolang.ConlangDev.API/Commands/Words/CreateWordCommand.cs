using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Myriolang.ConlangDev.API.Models;
using Myriolang.ConlangDev.API.Services;

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
        private readonly IWordService _wordService;
        public CreateWordCommandHandler(IWordService wordService) => _wordService = wordService;
        
        public async Task<Word> Handle(CreateWordCommand request, CancellationToken cancellationToken)
            => await _wordService
                .Create(request, cancellationToken)
                .ConfigureAwait(false);
    }
}