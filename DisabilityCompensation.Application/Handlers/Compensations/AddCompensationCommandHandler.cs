using AutoMapper;
using DisabilityCompensation.Application.Commands.Compensations;
using DisabilityCompensation.Application.Dtos.Entity;
using DisabilityCompensation.Domain.Entities;
using DisabilityCompensation.Domain.Interfaces.IServices;
using DisabilityCompensation.Shared.Dtos.Bases;
using DisabilityCompensation.Shared.Extensions;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace DisabilityCompensation.Application.Handlers.Compensations
{
    public class AddCompensationCommandHandler : IRequestHandler<AddCompensationCommand, BaseResponse<Guid>>
    {
        private readonly IMapper _mapper;
        private readonly ICompensationService _compensationService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AddCompensationCommandHandler(
            ICompensationService compensationService,
            IMapper mapper,
            IHttpContextAccessor httpContextAccessor)
        {
            _compensationService = compensationService;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<BaseResponse<Guid>> Handle(AddCompensationCommand request, CancellationToken cancellationToken)
        {
            var userClaim = _httpContextAccessor.HttpContext.GetClaims();
            var compensation = _mapper.Map<CompensationDto>(request);
            await _compensationService.AddAsync(compensation, userClaim);

            return new BaseResponse<Guid>
            {
                Data = compensation.Id,
                Succcess = true
            };
        }
    }
}
