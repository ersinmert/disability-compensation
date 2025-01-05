using DisabilityCompensation.Domain.Interfaces;
using DisabilityCompensation.Shared.Configurations;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;

namespace DisabilityCompensation.Infrastructure.FileUploaders
{
    public class LocalFileUploader : IFileUploader
    {
        private readonly string _uploadRootPath;
        private readonly FileUploadSettings _uploadSettings;

        public LocalFileUploader(IOptions<FileUploadSettings> fileUploadSettings)
        {
            _uploadSettings = fileUploadSettings.Value;

            _uploadRootPath = Path.Combine(Directory.GetCurrentDirectory(), _uploadSettings.LocalTargetPath!);

            if (!Directory.Exists(_uploadRootPath))
                Directory.CreateDirectory(_uploadRootPath);
        }

        public async Task<string> UploadAsync(IFormFile file)
        {
            if (file == null || file.Length == 0)
                throw new ArgumentException("Invalid file.");

            if (!Directory.Exists(_uploadRootPath))
                Directory.CreateDirectory(_uploadRootPath);

            string originalFileName = Path.GetFileNameWithoutExtension(file.FileName);
            string fileExtension = Path.GetExtension(file.FileName);
            string uniqueFileName = $"{originalFileName}_{Guid.NewGuid()}{fileExtension}";
            string filePath = Path.Combine(_uploadRootPath, uniqueFileName);
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            return Path.Combine(_uploadSettings.Host!, uniqueFileName).Replace("\\", "/");
        }
    }
}
