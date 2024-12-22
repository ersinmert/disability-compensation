using System.ComponentModel.DataAnnotations.Schema;

namespace DisabilityCompensation.Domain.Entities
{
    [Table("Parameter")]
    public class Parameter : BaseEntity
    {
        public required string Code { get; set; }

        public required string Name { get; set; }
        public required string Value { get; set; }
    }
}
