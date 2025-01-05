using Microsoft.AspNetCore.Http;

namespace DisabilityCompensation.Domain.Interfaces.IValidators
{
    public interface ICompositeFileValidator
    {
        Task ValidateAsync(IFormFile file);
    }
}
