using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Myriolang.ConlangDev.API.Models;
using Myriolang.ConlangDev.API.Services;

namespace Myriolang.ConlangDev.API.Queries.Words
{
    public class ListWordsByLanguageQuery : IRequest<IEnumerable<Word>>
    {
        public string Username { get; set; }
        public string LanguageSlug { get; set; }

        public ListWordsByLanguageQuery(string username, string languageSlug)
        {
            Username = username;
            LanguageSlug = languageSlug;
        }
    }

    public class ListWordsByLanguageQueryHandler : IRequestHandler<ListWordsByLanguageQuery, IEnumerable<Word>>
    {
        private readonly IWordService _wordService;
        public ListWordsByLanguageQueryHandler(IWordService wordService) => _wordService = wordService;

        public Task<IEnumerable<Word>> Handle(ListWordsByLanguageQuery request, CancellationToken cancellationToken)
            => _wordService.ListByProfileLanguage(request.Username, request.LanguageSlug, cancellationToken);
    }
}