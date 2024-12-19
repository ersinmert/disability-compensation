using DisabilityCompensation.Shared.Dtos.Bases;

namespace DisabilityCompensation.Shared.Dtos
{
    public class EventDto : BaseDto
    {
        public Guid CompensationId { get; set; }
        public string? EventType { get; set; }
        public DateTime EventDate { get; set; }
        public DateTime ExaminationDate { get; set; }
        public string? LifeStatus { get; set; }
        public string? Court { get; set; }
        public decimal? FaultRate { get; set; }
        public decimal? DisabilityRate { get; set; }
        public decimal? SgkAdvanceCapital { get; set; }
        public string? LifeTable { get; set; }
        public bool? IsFavorTransportDiscount { get; set; }
        public bool? IsMutualFaultDiscount { get; set; }
    }
}
