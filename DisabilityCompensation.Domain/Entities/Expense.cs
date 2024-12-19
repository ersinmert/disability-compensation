namespace DisabilityCompensation.Domain.Entities
{
    public class Expense : BaseEntity
    {
        public Guid CompensationId { get; set; }
        public required string ExpenseType { get; set; }
        public string? ReferenceNo { get; set; }
        public required DateTime Date { get; set; }
        public required decimal Amount { get; set; }
        public string? FilePath { get; set; }

        #region Relations

        public Compensation? Compensation { get; set; }

        #endregion
    }
}
