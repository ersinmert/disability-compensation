using System.ComponentModel.DataAnnotations.Schema;

namespace DisabilityCompensation.Domain.Entities
{
    [Table("Role")]
    public class Role : BaseEntity
    {
        public string? Name { get; set; }

        #region Relations

        public List<RoleAuthority>? RoleAuthorities { get; set; }
        public List<UserRole>? UserRoles { get; set; }

        #endregion
    }
}
