using System;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Myriolang.ConlangDev.API.Commands.Languages;
using Myriolang.ConlangDev.API.Commands.Profiles;
using Myriolang.ConlangDev.API.Mappers;
using Myriolang.ConlangDev.API.Models;
using Myriolang.ConlangDev.API.Models.Responses;
using Myriolang.ConlangDev.API.Queries.Profiles;

namespace Myriolang.ConlangDev.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProfileController : ControllerBase
    {
        private readonly IMediator _mediator;
        public ProfileController(IMediator mediator) => _mediator = mediator;

        [Authorize]
        [HttpGet]
        public async Task<ActionResult<Profile>> GetOwnProfile()
        {
            var id = User.Claims.FirstOrDefault(c => c.Type == "id")?.Value;
            var profile = await _mediator.Send(new FetchProfileQuery {Id = id});
            if (profile is not null)
                return Ok(profile);
            return NotFound();
        }
        
        [Authorize]
        [HttpGet("{username}")]
        public async Task<ActionResult> GetProfile([FromRoute] string username)
        {
            var profile = await _mediator.Send(new FetchProfileQuery {Username = username});
            if (profile is null)
                return NotFound();
            return User.IsInRole("ManageUsers") ? Ok(profile) : Ok(PublicProfile.FromProfile(profile));
        }
        
        [HttpPost]
        public async Task<ActionResult<Profile>> NewProfile([FromBody] CreateLanguageCommand createLanguageCommand)
        {
            var profile = await _mediator.Send(createLanguageCommand);
            if (profile is not null)
                return Ok(profile);
            return UnprocessableEntity();
        }

        [HttpPost("validate/username")]
        public async Task<ValidationResponse> ValidateUsername([FromBody] ValidateNewProfileUsernameQuery query)
            => await _mediator.Send(query);

        [HttpPost("validate/email")]
        public async Task<ValidationResponse> ValidateEmail([FromBody] ValidateNewProfileEmailQuery query)
            => await _mediator.Send(query);
    }
}