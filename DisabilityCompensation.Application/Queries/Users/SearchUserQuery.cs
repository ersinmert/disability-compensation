using DisabilityCompensation.Domain.Dtos;
using DisabilityCompensation.Shared.Dtos.Bases;
using MediatR;

namespace DisabilityCompensation.Application.Queries.Users
{
    public class SearchUserQuery : IRequest<BaseResponse<PagedResultDto<UserDto>>>
    {
        public int Page { get; set; }
        public int PageSize { get; set; }
        public string? Email { get; set; }
        public string? Name { get; set; }
        public string? Surname { get; set; }
    }
}
