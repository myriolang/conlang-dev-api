using System.Net.Mime;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Myriolang.ConlangDev.API.Commands.Authentication;
using Myriolang.ConlangDev.API.Models;
using Myriolang.ConlangDev.API.Models.Responses;

namespace Myriolang.ConlangDev.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Produces(MediaTypeNames.Application.Json)]
    [Consumes(MediaTypeNames.Application.Json)]
    public class AuthController : ControllerBase
    {
        private readonly IMediator _mediator;
        public AuthController(IMediator mediator) => _mediator = mediator;

        /// <summary>
        /// Dummy endpoint used to check that given token is valid, via the Authorize attribute
        /// </summary>
        [HttpGet]
        [Authorize]
        public ActionResult ValidateToken() => Ok();

        /// <summary>
        /// Validates an authentication query (username and password), returning
        /// a token and the profile if valid
        /// </summary>
        [HttpPost]
        public async Task<ActionResult<AuthenticationResponse>> Authenticate([FromBody] AuthenticationQuery authenticationQuery)
        {
            var response = await _mediator.Send(authenticationQuery);
            if (response is null) return Unauthorized();
            return Ok(response);
        }
    }
}