using Microsoft.AspNetCore.Http;

namespace DisabilityCompensation.Application.Dtos.Compensation.AddCompensation
{
    public class DocumentRequest
    {
        public string? DocumentType { get; set; }
        public string? ReferenceNo { get; set; }
        public DateTime Date { get; set; }
        public IFormFile? File { get; set; }
    }
}
