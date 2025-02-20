using System.ComponentModel.DataAnnotations.Schema;

namespace DisabilityCompensation.Domain.Entities
{
    [Table("RoleAuthority")]
    public class RoleAuthority : BaseEntity
    {
        public Guid RoleId { get; set; }
        public Guid AuthorityId { get; set; }

        #region Relations

        public Role? Role { get; set; }
        public Authority? Authority { get; set; }

        #endregion
    }
}
