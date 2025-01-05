namespace DisabilityCompensation.Shared.Configurations
{
    public class FileValidatorSettings
    {
        public List<string>? AllowedExtensions { get; set; }
        public List<string>? AllowedMimeTypes { get; set; }
        public int MaxFileSize { get; set; }
    }
}
