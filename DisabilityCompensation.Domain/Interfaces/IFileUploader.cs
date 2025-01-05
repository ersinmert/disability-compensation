using Microsoft.AspNetCore.Http;

namespace DisabilityCompensation.Domain.Interfaces
{
    public interface IFileUploader
    {
        Task<string> UploadAsync(IFormFile file);
    }
}
