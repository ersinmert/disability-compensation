using DisabilityCompensation.Application.Commands.Compensations;
using DisabilityCompensation.Application.Queries.Compensations;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DisabilityCompensation.API.Controllers
{
    [Route("api/compensations")]
    [ApiController]
    [Authorize]
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
            var response = await _mediator.Send(request);
            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromForm] AddCompensationCommand compensationRequest)
        {
            var response = await _mediator.Send(compensationRequest);
            return Ok(response);
        }
    }
}
