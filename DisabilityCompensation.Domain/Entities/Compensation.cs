﻿namespace DisabilityCompensation.Domain.Entities
{
    public class Compensation : BaseEntity
    {
        public string? PredefinedNote { get; set; }
        public string? Note { get; set; }

        #region Relations

        public required Claimant Claimant { get; set; }
        public required Event Event { get; set; }
        public List<Expense>? Expenses { get; set; }
        public List<Document>? Documents { get; set; }

        #endregion
    }
}