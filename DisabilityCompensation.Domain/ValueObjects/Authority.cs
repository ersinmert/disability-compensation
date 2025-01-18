using System.ComponentModel;

namespace DisabilityCompensation.Domain.ValueObjects
{
    public enum Authority
    {
        None = 0,

        [Description("İş Göremezlik Tazminatı")]
        DisabilityCompensation
    }
}
