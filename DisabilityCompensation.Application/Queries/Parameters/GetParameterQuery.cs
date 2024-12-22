using DisabilityCompensation.Application.Dtos.Parameter.GetParameter;
using DisabilityCompensation.Shared.Dtos.Bases;
using MediatR;

namespace DisabilityCompensation.Application.Queries.Parameters
{
    public class GetParameterQuery : IRequest<BaseResponse<List<ParameterResponse>>>
    {
        public List<string>? Codes { get; set; }
    }
}
