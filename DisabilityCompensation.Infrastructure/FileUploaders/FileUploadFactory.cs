using DisabilityCompensation.Domain.Interfaces;
using DisabilityCompensation.Shared.Configurations;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DisabilityCompensation.Infrastructure.FileUploaders
{
    public static class FileUploadFactory
    {
        public static IFileUploader CreateUploader(IServiceProvider serviceProvider, IConfiguration configuration)
        {
            var fileUploadSettings = configuration.GetSection(nameof(FileUploadSettings)).Get<FileUploadSettings>();

            return fileUploadSettings!.Provider switch
            {
                "Local" => serviceProvider.GetRequiredService<LocalFileUploader>(),
                _ => throw new NotImplementedException($"Unknown provider: {fileUploadSettings!.Provider}")
            };
        }
    }
}
