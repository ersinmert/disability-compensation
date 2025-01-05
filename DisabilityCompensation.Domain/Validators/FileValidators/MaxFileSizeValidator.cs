using DisabilityCompensation.Domain.Dtos;
using DisabilityCompensation.Domain.Interfaces.IValidators;
using DisabilityCompensation.Shared.Configurations;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;

namespace DisabilityCompensation.Domain.Validators.FileValidators
{
    public class MaxFileSizeValidator : IFileValidator
    {
        private readonly long _maxSizeInBytes;

        public MaxFileSizeValidator(IOptions<FileValidatorSettings> fileValidatorSettings)
        {
            _maxSizeInBytes = fileValidatorSettings.Value.MaxFileSize;
        }

        public async Task<ValidateDto> ValidateAsync(IFormFile file)
        {
            var validate = new ValidateDto();
            if (file.Length > _maxSizeInBytes)
            {
                validate.Message = $"Dosya boyutu {_maxSizeInBytes / 1024 / 1024} MB'yi geçemez.";
                return await Task.FromResult(validate);
            }

            validate.Success = true;
            return await Task.FromResult(validate);
        }
    }
}
