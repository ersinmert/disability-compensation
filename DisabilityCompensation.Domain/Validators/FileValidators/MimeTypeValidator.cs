using DisabilityCompensation.Domain.Dtos;
using DisabilityCompensation.Domain.Interfaces.IValidators;
using DisabilityCompensation.Shared.Configurations;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;

namespace DisabilityCompensation.Domain.Validators.FileValidators
{
    public class MimeTypeValidator : IFileValidator
    {
        private readonly List<string> _allowedMimeTypes;

        public MimeTypeValidator(IOptions<FileValidatorSettings> fileValidatorSettings)
        {
            _allowedMimeTypes = fileValidatorSettings.Value.AllowedMimeTypes!.ToList();
        }

        public async Task<ValidateDto> ValidateAsync(IFormFile file)
        {
            var validate = new ValidateDto();
            if (_allowedMimeTypes.Contains("*"))
            {
                return new ValidateDto
                {
                    Success = true
                };
            }

            if (!_allowedMimeTypes.Contains(file.ContentType))
            {
                validate.Message = $"Dosya türü yalnızca şu türlerden biri olabilir: {string.Join(", ", _allowedMimeTypes)}";
                return await Task.FromResult(validate);
            }

            validate.Success = true;
            return await Task.FromResult(validate);
        }
    }
}
