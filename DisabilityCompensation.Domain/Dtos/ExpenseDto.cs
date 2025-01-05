using DisabilityCompensation.Shared.Dtos.Bases;
using Microsoft.AspNetCore.Http;
using System.Text.Json.Serialization;

namespace DisabilityCompensation.Application.Dtos.Entity
{
    public class ExpenseDto : BaseDto
    {
        public Guid CompensationId { get; set; }
        public string? ExpenseType { get; set; }
        public string? ReferenceNo { get; set; }
        public DateTime Date { get; set; }
        public decimal Amount { get; set; }
        public string? FilePath { get; set; }

        [JsonIgnore]
        public IFormFile? File { get; set; }
    }
}
