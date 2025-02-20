using System.ComponentModel.DataAnnotations.Schema;

namespace DisabilityCompensation.Domain.Entities
{
    [Table("UserRole")]
    public class UserRole : BaseEntity
    {
        public Guid UserId { get; set; }
        public Guid RoleId { get; set; }

        #region Relations

        public User? User { get; set; }
        public Role? Role { get; set; }

        #endregion
    }
}
