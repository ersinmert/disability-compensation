using DisabilityCompensation.Domain.Interfaces;
using DisabilityCompensation.Domain.Interfaces.IServices;
using DisabilityCompensation.Domain.Interfaces.IValidators;
using Microsoft.AspNetCore.Http;

namespace DisabilityCompensation.Domain.Services
{
    public class FileUploaderService : IFileUploaderService
    {
        private readonly IFileUploader _fileUploader;
        private readonly ICompositeFileValidator _compositeFileValidator;

        public FileUploaderService(
            IFileUploader fileUploader,
            ICompositeFileValidator compositeFileValidator)
        {
            _fileUploader = fileUploader;
            _compositeFileValidator = compositeFileValidator;
        }

        public async Task<string> UploadFileAsync(IFormFile file)
        {
            await _compositeFileValidator.ValidateAsync(file);
            return await _fileUploader.UploadAsync(file);
        }
    }
}
