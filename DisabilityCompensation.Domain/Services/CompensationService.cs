using AutoMapper;
using DisabilityCompensation.Application.Dtos.Entity;
using DisabilityCompensation.Domain.Entities;
using DisabilityCompensation.Domain.Interfaces;
using DisabilityCompensation.Domain.Interfaces.IRepositories;
using DisabilityCompensation.Domain.Interfaces.IServices;
using DisabilityCompensation.Shared.Dtos;
using Microsoft.AspNetCore.Http;

namespace DisabilityCompensation.Domain.Services
{
    public class CompensationService : GenericService<ICompensationRepository, Compensation>, ICompensationService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IFileUploaderService _fileUploaderService;

        public CompensationService(
            IUnitOfWork unitOfWork,
            IMapper mapper,
            IFileUploaderService fileUploaderService) : base(unitOfWork.CompensationRepository, mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _fileUploaderService = fileUploaderService;
        }

        public async Task<Guid> AddAsync(CompensationDto compensationDto, UserClaim userClaim)
        {
            await UploadDocumentFiles(compensationDto.Documents);
            await UploadExpenseFiles(compensationDto.Expenses);

            var compensation = _mapper.Map<Compensation>(compensationDto);
            compensation.CreatedBy = userClaim.UserId;
            await _unitOfWork.CompensationRepository.AddAsync(compensation);

            await _unitOfWork.SaveChangesAsync();

            return compensationDto.Id;
        }

        private async Task UploadDocumentFiles(List<DocumentDto>? documents)
        {
            if (documents?.Any() != true)
                return;

            foreach (var document in documents)
            {
                if (document.File == null)
                    continue;

                var uploadedPath = await _fileUploaderService.UploadFileAsync(document.File);
                document.FilePath = uploadedPath;
            }
        }

        private async Task UploadExpenseFiles(List<ExpenseDto>? expenses)
        {
            if (expenses?.Any() != true)
                return;

            foreach (var expense in expenses)
            {
                if (expense.File == null)
                    continue;

                var uploadedPath = await _fileUploaderService.UploadFileAsync(expense.File);
                expense.FilePath = uploadedPath;
            }
        }
    }
}
