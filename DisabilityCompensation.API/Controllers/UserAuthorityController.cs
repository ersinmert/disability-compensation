using DisabilityCompensation.Application.Attributes;
using DisabilityCompensation.Application.Queries.UserAuthorities;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DisabilityCompensation.API.Controllers
{
    [Route("api/users/{userId}/authorities")]
    [ApiController]
    [Authorize]
    public class UserAuthorityController : ControllerBase
    {
        private readonly IMediator _mediator;

        public UserAuthorityController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromRoute] GetUserAuthoritiesQuery request)
        {
            var response = await _mediator.Send(request);
            return Ok(response);
        }
    }
}
