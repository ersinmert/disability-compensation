using DisabilityCompensation.Shared.Dtos;
using DisabilityCompensation.Shared.Dtos.Bases;
using MediatR;

namespace DisabilityCompensation.Application.Commands.Users
{
    public class DeleteUserCommand : IRequest<BaseResponse<bool>>
    {
        public Guid Id { get; set; }

        public UserClaim? UserClaim { get; set; }
    }
}
