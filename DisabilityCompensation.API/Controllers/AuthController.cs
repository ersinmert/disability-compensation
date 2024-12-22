using DisabilityCompensation.Application.Commands.Auth;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace DisabilityCompensation.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AuthController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] AuthLoginQuery request)
        {
            var response = await _mediator.Send(request);
            return Ok(response);
        }
    }
}
