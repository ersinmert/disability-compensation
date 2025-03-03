using AutoMapper;
using DisabilityCompensation.Application.Dtos.Entity;
using DisabilityCompensation.Domain.Dtos;
using DisabilityCompensation.Domain.Entities;
using DisabilityCompensation.Domain.Interfaces;
using DisabilityCompensation.Domain.Interfaces.IRepositories;
using DisabilityCompensation.Domain.Interfaces.IServices;
using DisabilityCompensation.Domain.Interfaces.IServices.CompensationCalculator;
using DisabilityCompensation.Shared.Dtos;

namespace DisabilityCompensation.Domain.Services
{
    public class CompensationService : GenericService<ICompensationRepository, Compensation>, ICompensationService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IFileUploaderService _fileUploaderService;
        private readonly ICompensationCalculationManager _compensationCalculationManager;

        public CompensationService(
            IUnitOfWork unitOfWork,
            IMapper mapper,
            IFileUploaderService fileUploaderService,
            ICompensationCalculationManager compensationCalculationManager) : base(unitOfWork.CompensationRepository, mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _fileUploaderService = fileUploaderService;
            _compensationCalculationManager = compensationCalculationManager;
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

        public async Task<PagedResultDto<CompensationDto>> SearchPagedAsync(SearchCompensationDto search, UserClaim userClaim)
        {
            var roles = await _unitOfWork.UserRoleRepository.GetRolesAsync(userClaim.UserId);
            var hasAllDataAccess = roles.Any(role =>
                                        role == ValueObjects.Role.Admin
                                        ||
                                        role == ValueObjects.Role.Expert
                                   );
            PagedResult<Compensation>? data;
            if (hasAllDataAccess)
            {
                data = await _unitOfWork.CompensationRepository.SearchAllPagedAsync(search);
            }
            else
            {
                data = await _unitOfWork.CompensationRepository.SearchOwnedPagedAsync(search, userClaim.UserId);
            }
            return _mapper.Map<PagedResultDto<CompensationDto>>(data);
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

        public async Task<bool> ApproveAsync(ApproveCompensationDto approveDto, UserClaim userClaim)
        {
            var compensation = await _unitOfWork.CompensationRepository.GetByIdAsync(approveDto.Id);
            compensation!.UpdatedDate = DateTime.UtcNow;
            compensation.UpdatedBy = userClaim.UserId;
            compensation.Status = ValueObjects.CompensationStatus.Approve;
            compensation.HasTemporaryDisability = approveDto.HasTemporaryDisability;
            compensation.TemporaryDisabilityDay = approveDto.TemporaryDisabilityDay;
            compensation.Event.DisabilityRate = approveDto.DisabilityRate;

            compensation.HasCaregiver = approveDto.HasCaregiver;

            await _unitOfWork.SaveChangesAsync();

            return true;
        }

        public async Task<bool> RejectAsync(RejectCompensationDto rejectDto, UserClaim userClaim)
        {
            var compensation = await _unitOfWork.CompensationRepository.FirstOrDefaultAsync(x => x.Id == rejectDto.Id, tracking: true);
            compensation!.UpdatedDate = DateTime.UtcNow;
            compensation.UpdatedBy = userClaim.UserId;
            compensation.Status = ValueObjects.CompensationStatus.Reject;
            compensation.RejectReason = rejectDto.RejectReason;

            await _unitOfWork.SaveChangesAsync();

            return true;
        }

        public async Task CalculateAsync(Guid compensationId)
        {
            await _unitOfWork.BeginTransactionAsync();
            try
            {
                var totalAmount = await _compensationCalculationManager.CalculateAsync(compensationId);

                await _unitOfWork.SaveChangesAsync();
                await _unitOfWork.CommitAsync();
            }
            catch
            {
                await _unitOfWork.RollbackAsync();

                throw;
            }
        }
    }
}
