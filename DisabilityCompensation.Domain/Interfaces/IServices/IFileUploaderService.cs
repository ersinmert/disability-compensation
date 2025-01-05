using Microsoft.AspNetCore.Http;

namespace DisabilityCompensation.Domain.Interfaces.IServices
{
    public interface IFileUploaderService
    {
        Task<string> UploadFileAsync(IFormFile file);
    }
}
