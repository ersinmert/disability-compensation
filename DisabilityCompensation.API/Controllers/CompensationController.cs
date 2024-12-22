using DisabilityCompensation.Application.Commands.Compensations;
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
        public async Task<IActionResult> Get([FromRoute] GetCompensationQuery request)
        {
            var response = await _mediator.Send(request);
            return Ok(response);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var response = await _mediator.Send(new GetAllCompensationQuery());
            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] AddCompensationCommand compensationRequest)
        {
            var response = await _mediator.Send(compensationRequest);
            return Ok(response);
        }
    }
}
