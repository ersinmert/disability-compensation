using DisabilityCompensation.Shared.Dtos;
using DisabilityCompensation.Shared.Dtos.Bases;
using MediatR;
using Swashbuckle.AspNetCore.Annotations;

namespace DisabilityCompensation.Application.Commands.Users
{
    public class DeleteUserCommand : IRequest<BaseResponse<bool>>
    {
        public Guid Id { get; set; }

        [SwaggerIgnore]
        public UserClaim? UserClaim { get; set; }
    }
}
