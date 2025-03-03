using DisabilityCompensation.Application.Attributes;
using DisabilityCompensation.Application.Commands.Compensations;
using DisabilityCompensation.Application.Queries.Compensations;
using DisabilityCompensation.Domain.ValueObjects;
using DisabilityCompensation.Shared.Extensions;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DisabilityCompensation.API.Controllers
{
    [Route("api/compensations")]
    [ApiController]
    [Authorize]
    [Authority(Authority.DisabilityCompensation)]
    public class CompensationController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CompensationController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get([FromRoute] GetCompensationQuery request)
        {
            var response = await _mediator.Send(request);
            return Ok(response);
        }

        [HttpGet]
        public async Task<IActionResult> Search([FromQuery] SearchCompensationQuery request)
        {
            request.UserClaim = User.GetClaims();
            var response = await _mediator.Send(request);
            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromForm][FromBody] AddCompensationCommand request)
        {
            var files = Request.Form.Files;
            request.UserClaim = User.GetClaims();
            var response = await _mediator.Send(request);
            return Ok(response);
        }

        [HttpPatch("{id}/approve")]
        [Authority(Authority.DisabilityCompensationStatus)]
        public async Task<IActionResult> Approve([FromBody][FromRoute] ApproveCompensationCommand request)
        {
            request.UserClaim = User.GetClaims();
            var response = await _mediator.Send(request);
            return Ok(response);
        }

        [HttpPatch("{id}/reject")]
        [Authority(Authority.DisabilityCompensationStatus)]
        public async Task<IActionResult> Reject([FromBody][FromRoute] RejectCompensationCommand request)
        {
            request.UserClaim = User.GetClaims();
            var response = await _mediator.Send(request);
            return Ok(response);
        }
    }
}
