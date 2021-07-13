using System;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Myriolang.ConlangDev.API.Commands.Profiles;
using Myriolang.ConlangDev.API.Models;
using Myriolang.ConlangDev.API.Models.Responses;

namespace Myriolang.ConlangDev.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProfileController : ControllerBase
    {
        private readonly IMediator _mediator;
        public ProfileController(IMediator mediator) => _mediator = mediator;

        [HttpPost]
        public async Task<ActionResult<Profile>> NewProfile([FromBody] NewProfileMutation newProfileMutation)
        {
            var profile = await _mediator.Send(newProfileMutation);
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