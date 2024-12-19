using DisabilityCompensation.Application.Queries.Compensations;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace DisabilityCompensation.API.Controllers
{
    [Route("api/compensations")]
    [ApiController]
    public class CompensationController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CompensationController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            var response = await _mediator.Send(new GetCompensationQuery { CompensationId = id });
            return Ok(response);
        }
    }
}
