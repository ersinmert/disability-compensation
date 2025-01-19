using DisabilityCompensation.Application.Attributes;
using DisabilityCompensation.Application.Queries.Authorities;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DisabilityCompensation.API.Controllers
{
    [Route("api/authorities")]
    [ApiController]
    [Authorize]
    [Admin]
    public class AuthorityController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AuthorityController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] GetAuthoritiesQuery request)
        {
            var response = await _mediator.Send(request);
            return Ok(response);
        }
    }
}
