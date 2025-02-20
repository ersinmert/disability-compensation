using System.ComponentModel;

namespace DisabilityCompensation.Domain.ValueObjects
{
    public enum Authority
    {
        None = 0,

        [Description("İş Göremezlik Tazminatı")]
        DisabilityCompensation,

        [Description("İş Göremezlik Tazminat Durumu")]
        DisabilityCompensationStatus
    }
}
