using IslamicPOS.Domain.Finance;
using IslamicPOS.Domain.Common;
using Microsoft.EntityFrameworkCore;

namespace IslamicPOS.Infrastructure.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<ZakaatCalculation> ZakaatCalculations => Set<ZakaatCalculation>();
        public DbSet<IslamicFinanceOptions> FinanceOptions => Set<IslamicFinanceOptions>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<ZakaatCalculation>()
                .OwnsOne(z => z.TotalWealth, m =>
                {
                    m.Property(p => p.Amount).HasColumnName("TotalWealthAmount");
                    m.Property(p => p.Currency).HasColumnName("TotalWealthCurrency");
                });

            modelBuilder.Entity<ZakaatCalculation>()
                .OwnsOne(z => z.NisabThreshold, m =>
                {
                    m.Property(p => p.Amount).HasColumnName("NisabThresholdAmount");
                    m.Property(p => p.Currency).HasColumnName("NisabThresholdCurrency");
                });

            modelBuilder.Entity<ZakaatCalculation>()
                .OwnsOne(z => z.ZakaatAmount, m =>
                {
                    m.Property(p => p.Amount).HasColumnName("ZakaatAmount");
                    m.Property(p => p.Currency).HasColumnName("ZakaatCurrency");
                });

            // Seed initial finance options
            modelBuilder.Entity<IslamicFinanceOptions>().HasData(
                new IslamicFinanceOptions(
                    nisabThreshold: new Domain.ValueObjects.Money(5000m, "USD"),
                    zakaatRate: 0.025m,
                    defaultProfitSharingRatio: 0.7m,
                    financingTermInMonths: 12
                )
            );
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            foreach (var entry in ChangeTracker.Entries<BaseEntity>())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.Created = DateTime.UtcNow;
                        break;

                    case EntityState.Modified:
                        entry.Entity.LastModified = DateTime.UtcNow;
                        break;
                }
            }

            return base.SaveChangesAsync(cancellationToken);
        }
    }
}