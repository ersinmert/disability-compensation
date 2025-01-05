using DisabilityCompensation.Domain.Dtos;
using DisabilityCompensation.Domain.Interfaces.IValidators;
using DisabilityCompensation.Shared.Configurations;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;

namespace DisabilityCompensation.Domain.Validators.FileValidators
{
    public class FileExtensionValidator : IFileValidator
    {
        private readonly List<string> _allowedExtensions;

        public FileExtensionValidator(IOptions<FileValidatorSettings> fileValidatorSettings)
        {
            _allowedExtensions = fileValidatorSettings.Value.AllowedExtensions!.Select(e => e.ToLowerInvariant()).ToList();
        }

        public async Task<ValidateDto> ValidateAsync(IFormFile file)
        {
            var validate = new ValidateDto();
            if (_allowedExtensions.Contains("*"))
            {
                return new ValidateDto
                {
                    Success = true
                };
            }

            string fileExtension = Path.GetExtension(file.FileName).ToLowerInvariant();
            if (!_allowedExtensions.Contains(fileExtension))
            {
                validate.Message = $"Yalnızca şu uzantılar destekleniyor: {string.Join(", ", _allowedExtensions)}";
                return await Task.FromResult(validate);
            }

            validate.Success = true;
            return await Task.FromResult(validate);
        }
    }
}
