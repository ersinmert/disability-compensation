using AutoMapper;
using DisabilityCompensation.Application.Commands.Compensations;
using DisabilityCompensation.Application.Dtos.Compensation.AddCompensation;
using DisabilityCompensation.Application.Dtos.Entity;
using DisabilityCompensation.Domain.Dtos;
using DisabilityCompensation.Domain.Entities;

namespace DisabilityCompensation.Application.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<User, UserDto>().ReverseMap();
            CreateMap<Compensation, CompensationDto>().ReverseMap();
            CreateMap<Parameter, ParameterDto>().ReverseMap();
            CreateMap<Expense, ExpenseDto>().ReverseMap();
            CreateMap<Event, EventDto>().ReverseMap();
            CreateMap<Document, DocumentDto>().ReverseMap();
            CreateMap<Claimant, ClaimantDto>().ReverseMap();
            CreateMap<AddCompensationCommand, CompensationDto>().ReverseMap();
            CreateMap<EventRequest, EventDto>().ReverseMap();
            CreateMap<ExpenseRequest, ExpenseDto>().ReverseMap();
            CreateMap<DocumentRequest, DocumentDto>().ReverseMap();
            CreateMap<ClaimantRequest, ClaimantDto>().ReverseMap();
        }
    }
}
