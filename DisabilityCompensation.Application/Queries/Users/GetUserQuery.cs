using DisabilityCompensation.Domain.Dtos;
using DisabilityCompensation.Shared.Dtos.Bases;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace DisabilityCompensation.Application.Queries.Users
{
    public class GetUserQuery : IRequest<BaseResponse<UserDto>>
    {
        [FromRoute(Name = "id")]
        public Guid Id { get; set; }
    }
}
