using AutoMapper;
using DisabilityCompensation.Domain.Entities;
using DisabilityCompensation.Shared.Dtos;

namespace DisabilityCompensation.Application.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Compensation, CompensationDto>().ReverseMap();
        }
    }
}
