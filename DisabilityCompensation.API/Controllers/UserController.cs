using DisabilityCompensation.Application.Attributes;
using DisabilityCompensation.Application.Commands.Users;
using DisabilityCompensation.Application.Queries.Users;
using DisabilityCompensation.Shared.Extensions;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DisabilityCompensation.API.Controllers
{
    [Route("api/users")]
    [ApiController]
    [Authorize]
    [Admin]
    public class UserController : ControllerBase
    {
        private readonly IMediator _mediator;

        public UserController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get([FromRoute] GetUserQuery request)
        {
            var response = await _mediator.Send(request);
            return Ok(response);
        }

        [HttpGet]
        public async Task<IActionResult> Search([FromQuery] SearchUserQuery request)
        {
            var response = await _mediator.Send(request);
            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] AddUserCommand request)
        {
            request.UserClaim = User.GetClaims();
            var response = await _mediator.Send(request);
            return CreatedAtAction(nameof(Get), new { id = response.Data }, response);
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] UpdateUserCommand request)
        {
            request.UserClaim = User.GetClaims();
            var response = await _mediator.Send(request);
            return Ok(response);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete([FromQuery] DeleteUserCommand request)
        {
            request.UserClaim = User.GetClaims();
            var response = await _mediator.Send(request);
            return Ok(response);
        }
    }
}
