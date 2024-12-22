using DisabilityCompensation.Application.Dtos.Entity;
using DisabilityCompensation.Application.Dtos.Parameter.GetParameter;
using DisabilityCompensation.Application.Queries.Parameters;
using DisabilityCompensation.Domain.Interfaces.IServices;
using DisabilityCompensation.Shared.Dtos.Bases;
using MediatR;

namespace DisabilityCompensation.Application.Handlers.Parameters
{
    public class GetParametersQueryHandler : IRequestHandler<GetParameterQuery, BaseResponse<List<ParameterResponse>>>
    {
        private readonly IParameterService _parameterService;

        public GetParametersQueryHandler(IParameterService parameterService)
        {
            _parameterService = parameterService;
        }

        public async Task<BaseResponse<List<ParameterResponse>>> Handle(GetParameterQuery request, CancellationToken cancellationToken)
        {
            var parameters = await _parameterService.GetParameters(request.Codes);
            var parameterResponses = TransformData(parameters);

            return new BaseResponse<List<ParameterResponse>>
            {
                Data = parameterResponses,
                Succcess = true
            };
        }

        private List<ParameterResponse> TransformData(List<ParameterDto> parameters)
        {
            return parameters.GroupBy(x => x.Code).Select(group => new ParameterResponse
            {
                Code = group.Key,
                Values = group.Select(x => new ParameterValueResponse
                {
                    Name = x.Name,
                    Value = x.Value
                }).ToList()
            }).ToList();
        }
    }
}
