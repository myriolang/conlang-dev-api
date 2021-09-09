using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Myriolang.ConlangDev.API.Models;
using Myriolang.ConlangDev.API.Services;

namespace Myriolang.ConlangDev.API.Commands.Languages
{
    public class CreateLanguageCommand : IRequest<Language>
    {
        [BsonRepresentation(BsonType.ObjectId)]
        public string ProfileId { get; set; }
        [Required]
        public string Name { get; set; }
        public string NativeName { get; set; }
        [Required]
        public string Slug { get; set; }
        public string Description { get; set; }
        public List<string> Tags { get; set; }
    }
    
    public class CreateLanguageCommandHandler : IRequestHandler<CreateLanguageCommand, Language>
    {
        private readonly ILanguageService _languageService;
        public CreateLanguageCommandHandler(ILanguageService languageService) => _languageService = languageService;

        public Task<Language> Handle(CreateLanguageCommand request, CancellationToken cancellationToken)
            => _languageService.Create(request, cancellationToken);
    }
}