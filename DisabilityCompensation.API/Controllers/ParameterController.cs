﻿using DisabilityCompensation.Application.Attributes;
using DisabilityCompensation.Application.Commands.Parameters;
using DisabilityCompensation.Application.Queries.Parameters;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DisabilityCompensation.API.Controllers
{
    [Route("api/parameters")]
    [ApiController]
    [Authorize]
    public class ParameterController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ParameterController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("search")]
        public async Task<IActionResult> Search([FromBody] GetParameterQuery request)
        {
            var response = await _mediator.Send(request);
            return Ok(response);
        }

        [HttpPost]
        [Admin]
        public async Task<IActionResult> Add([FromBody] AddParametersCommand request)
        {
            var response = await _mediator.Send(request);
            return Ok(response);
        }
    }
}
