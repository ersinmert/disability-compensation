using System.ComponentModel;

namespace DisabilityCompensation.Domain.ValueObjects
{
    public enum Role
    {
        None = 0,

        [Description("Admin")]
        Admin = 1,

        [Description("Bilir kişi")]
        Expert = 2,

        [Description("Aktüerya")]
        Actuary = 3
    }
}
