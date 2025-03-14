﻿using DisabilityCompensation.Shared.Dtos;
using DisabilityCompensation.Shared.Dtos.Bases;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace DisabilityCompensation.Application.Commands.Compensations
{
    public class ApproveCompensationCommand : IRequest<BaseResponse<bool>>
    {
        [FromRoute(Name = "id")]
        public Guid Id { get; set; }
        public int DisabilityRate { get; set; }
        public bool HasTemporaryDisability { get; set; }
        public bool HasCaregiver { get; set; }
        public int TemporaryDisabilityDay { get; set; }

        [SwaggerIgnore]
        public UserClaim? UserClaim { get; set; }
    }
}
