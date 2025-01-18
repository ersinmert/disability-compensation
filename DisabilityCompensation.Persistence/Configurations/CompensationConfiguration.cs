using DisabilityCompensation.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DisabilityCompensation.Persistence.Configurations
{
    public class CompensationConfiguration : IEntityTypeConfiguration<Compensation>
    {
        public void Configure(EntityTypeBuilder<Compensation> builder)
        {
            builder.HasOne(p => p.CreatedByUser)
               .WithMany(u => u.Compensations)
               .HasForeignKey(p => p.CreatedBy);
        }
    }
}
