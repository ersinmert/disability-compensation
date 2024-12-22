using AutoMapper;
using DisabilityCompensation.Application.Commands.Parameters;
using DisabilityCompensation.Application.Dtos.Entity;
using DisabilityCompensation.Domain.Interfaces.IServices;
using DisabilityCompensation.Shared.Dtos.Bases;
using MediatR;

namespace DisabilityCompensation.Application.Handlers.Parameters
{
    public class AddParametersCommandHandler : IRequestHandler<AddParametersCommand, BaseResponse<List<Guid>>>
    {
        private readonly IParameterService _parameterService;
        private readonly IMapper _mapper;

        public AddParametersCommandHandler(
            IParameterService parameterService,
            IMapper mapper)
        {
            _parameterService = parameterService;
            _mapper = mapper;
        }

        public async Task<BaseResponse<List<Guid>>> Handle(AddParametersCommand request, CancellationToken cancellationToken)
        {
            var parameters = TransformData(request);
            var ids = await _parameterService.AddAsync(parameters);

            return new BaseResponse<List<Guid>>
            {
                Data = ids,
                Succcess = ids?.Any() == true
            };
        }

        private List<ParameterDto> TransformData(AddParametersCommand request)
        {
            List<ParameterDto> parameters = new List<ParameterDto>();
            foreach (var parameter in request.Parameters!)
            {
                parameters.AddRange(parameter.Values!.Select(parameterValue => new ParameterDto
                {
                    Code = parameter.Code,
                    Name = parameterValue.Name,
                    Value = parameterValue.Value
                }).ToList());
            }
            return parameters;
        }
    }
}
