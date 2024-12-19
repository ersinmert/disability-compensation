using AutoMapper;
using DisabilityCompensation.Application.Interfaces;
using DisabilityCompensation.Shared.Dtos;
using DisabilityCompensation.Shared.Dtos.Bases;
using MediatR;

namespace DisabilityCompensation.Application.Queries.Compensations
{
    public class GetCompensationHandler : IRequestHandler<GetCompensationQuery, BaseResponse<CompensationDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetCompensationHandler(
            IUnitOfWork unitOfWork,
            IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public Task<BaseResponse<CompensationDto>> Handle(GetCompensationQuery request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
