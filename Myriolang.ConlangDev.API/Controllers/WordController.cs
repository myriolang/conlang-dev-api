using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Myriolang.ConlangDev.API.Commands.Words;
using Myriolang.ConlangDev.API.Models;
using Myriolang.ConlangDev.API.Queries.Words;

namespace Myriolang.ConlangDev.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class WordController : ControllerBase
    {
        private readonly IMediator _mediator;
        public WordController(IMediator mediator) => _mediator = mediator;

        [HttpGet("{username}/{languageSlug}")]
        public async Task<ActionResult<IEnumerable<Word>>> GetByLanguage([FromRoute] string username,
            [FromRoute] string languageSlug)
        {
            var words = await _mediator.Send(new ListWordsByLanguageQuery(username, languageSlug));
            if (words is not null) return Ok(words);
            return NotFound();
        }

        [HttpGet("{username}/{languageSlug}/{id}")]
        public async Task<ActionResult<Word>> GetById([FromRoute] string username, [FromRoute] string languageSlug,
            [FromRoute] string id)
        {
            var word = await _mediator.Send(new GetWordByLanguageQuery(username, languageSlug, id));
            if (word is not null) return Ok(word);
            return NotFound();
        }

        [HttpPost]
        public async Task<ActionResult<Word>> Create([FromBody] CreateWordCommand createWordCommand)
        {
            var word = await _mediator.Send(createWordCommand);
            if (word is not null) return Ok(word);
            return UnprocessableEntity();
        }
    }
}