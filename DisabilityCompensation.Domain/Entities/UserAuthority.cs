using System.ComponentModel.DataAnnotations.Schema;

namespace DisabilityCompensation.Domain.Entities
{
    [Table("UserAuthority")]
    public class UserAuthority : BaseEntity
    {
        public Guid AuthorityId { get; set; }
        public Guid UserId { get; set; }

        #region MyRegion

        public User? User { get; set; }
        public Authority? Authority { get; set; }

        #endregion
    }
}
