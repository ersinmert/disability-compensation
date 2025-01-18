using DisabilityCompensation.Domain.Entities;
using DisabilityCompensation.Persistence.Configurations;
using Microsoft.EntityFrameworkCore;

namespace DisabilityCompensation.Persistence.Contexts
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new CompensationConfiguration());
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Claimant> Claimants { get; set; }
        public DbSet<Compensation> Compensations { get; set; }
        public DbSet<Document> Documents { get; set; }
        public DbSet<Event> Events { get; set; }
        public DbSet<Expense> Expenses { get; set; }
        public DbSet<Parameter> Parameters { get; set; }
    }
}
