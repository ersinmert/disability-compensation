using DisabilityCompensation.Domain.Dtos;
using Microsoft.AspNetCore.Http;

namespace DisabilityCompensation.Domain.Interfaces.IValidators
{
    public interface IFileValidator
    {
        Task<ValidateDto> ValidateAsync(IFormFile file);
    }
}
