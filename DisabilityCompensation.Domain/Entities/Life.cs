using System.ComponentModel.DataAnnotations.Schema;

namespace DisabilityCompensation.Domain.Entities
{
    [Table("Life")]
    public class Life : BaseEntity
    {
        public string? LifeType { get; set; }
        public string? Gender { get; set; }
        public int CurrentAge { get; set; }
        public decimal EstimatedYear { get; set; }
    }
}
