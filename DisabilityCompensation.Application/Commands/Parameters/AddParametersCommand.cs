using DisabilityCompensation.Application.Dtos.Parameter.AddParameter;
using DisabilityCompensation.Shared.Dtos.Bases;
using MediatR;

namespace DisabilityCompensation.Application.Commands.Parameters
{
    public class AddParametersCommand : IRequest<BaseResponse<List<Guid>>>
    {
        public List<ParameterRequest>? Parameters { get; set; }
    }
}
