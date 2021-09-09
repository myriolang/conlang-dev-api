using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Myriolang.ConlangDev.API.Commands.Words;
using Myriolang.ConlangDev.API.Models;

namespace Myriolang.ConlangDev.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class WordController : ControllerBase
    {
        private readonly IMediator _mediator;
        public WordController(IMediator mediator) => _mediator = mediator;

        [HttpGet("{id}")]
        public Task<ActionResult<Word>> GetById([FromRoute] string id)
        {
            throw new System.NotImplementedException();
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