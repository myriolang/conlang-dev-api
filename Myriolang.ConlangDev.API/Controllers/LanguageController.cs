using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Myriolang.ConlangDev.API.Commands.Languages;
using Myriolang.ConlangDev.API.Models;
using Myriolang.ConlangDev.API.Models.Responses;
using Myriolang.ConlangDev.API.Queries.Languages;

namespace Myriolang.ConlangDev.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class LanguageController : ControllerBase
    {
        private readonly IMediator _mediator;
        public LanguageController(IMediator mediator) => _mediator = mediator;

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Language>>> Get()
        {
            var profileId = User.Claims.FirstOrDefault(c => c.Type == "id")?.Value;
            var languages = await _mediator.Send(new FetchProfileLanguageQuery {ProfileId = profileId});
            if (languages is not null)
                return Ok(languages);

            return NoContent();
        }

        [HttpPost]
        public async Task<ActionResult<Language>> NewLanguage([FromBody] CreateLanguageCommand createLanguageCommand)
        {
            createLanguageCommand.ProfileId = User.Claims.FirstOrDefault(c => c.Type == "id")?.Value;
            if (string.IsNullOrEmpty(createLanguageCommand.ProfileId))
                return Unauthorized();

            var language = await _mediator.Send(createLanguageCommand);
            if (language is not null)
                return Ok(language);
            return UnprocessableEntity();
        }

        [HttpPost("validate/slug")]
        public async Task<ActionResult<ValidationResponse>> ValidateSlug([FromBody] ValidateNewLanguageSlugQuery query)
        {
            query.ProfileId = User.Claims.FirstOrDefault(c => c.Type == "id")?.Value;
            return await _mediator.Send(query);
        }
}
}