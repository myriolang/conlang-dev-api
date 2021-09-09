using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Myriolang.ConlangDev.API.Models;
using Myriolang.ConlangDev.API.Services;

namespace Myriolang.ConlangDev.API.Queries.Words
{
    public class GetWordByLanguageQuery : IRequest<Word>
    {
        public string Username { get; set; }
        public string LanguageSlug { get; set; }
        public string Id { get; set; }

        public GetWordByLanguageQuery(string username, string languageSlug, string id)
        {
            Username = username;
            LanguageSlug = languageSlug;
            Id = id;
        }
    }

    public class GetWordByLanguageQueryHandler : IRequestHandler<GetWordByLanguageQuery, Word>
    {
        private readonly IWordService _wordService;
        public GetWordByLanguageQueryHandler(IWordService wordService) => _wordService = wordService;

        public Task<Word> Handle(GetWordByLanguageQuery request, CancellationToken cancellationToken)
            => _wordService.FindByProfileLanguageId(request.Username, request.LanguageSlug, request.Id,
                cancellationToken);
    }
}