using Microsoft.AspNetCore.Http;

namespace DisabilityCompensation.Application.Dtos.Compensation.AddCompensation
{
    public class ExpenseRequest
    {
        public string? ExpenseType { get; set; }
        public string? ReferenceNo { get; set; }
        public DateTime Date { get; set; }
        public decimal Amount { get; set; }
        public IFormFile? File { get; set; }
    }
}
