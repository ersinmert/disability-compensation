namespace DisabilityCompensation.Domain.Entities
{
    public class Document : BaseEntity
    {
        public Guid CompensationId { get; set; }
        public required string DocumentType { get; set; }
        public string? ReferenceNo { get; set; }
        public required DateTime Date { get; set; }
        public required string FilePath { get; set; }

        #region Relations
        public Compensation? Compensation { get; set; }

        #endregion
    }
}
