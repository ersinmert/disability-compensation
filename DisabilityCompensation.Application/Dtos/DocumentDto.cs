using DisabilityCompensation.Shared.Dtos.Bases;

namespace DisabilityCompensation.Shared.Dtos
{
    public class DocumentDto : BaseDto
    {
        public Guid CompensationId { get; set; }
        public required string DocumentType { get; set; }
        public string? ReferenceNo { get; set; }
        public required DateTime Date { get; set; }
        public required string FilePath { get; set; }
    }
}
