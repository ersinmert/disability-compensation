using System.ComponentModel.DataAnnotations.Schema;

namespace DisabilityCompensation.Domain.Entities
{
    [Table("Authority")]
    public class Authority : BaseEntity
    {
        public string? Name { get; set; }
    }
}
