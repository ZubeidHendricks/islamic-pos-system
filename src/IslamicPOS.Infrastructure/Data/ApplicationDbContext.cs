using IslamicPOS.Domain.Sales;
using IslamicPOS.Domain.Finance;
using IslamicPOS.Domain.Inventory;
using IslamicPOS.Domain.Common;
using IslamicPOS.Domain.Logistics;
using Microsoft.EntityFrameworkCore;

namespace IslamicPOS.Infrastructure.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<Sale> Sales => Set<Sale>();
    public DbSet<SaleItem> SaleItems => Set<SaleItem>();
    public DbSet<Product> Products => Set<Product>();
    public DbSet<Category> Categories => Set<Category>();
    public DbSet<Partner> Partners => Set<Partner>();
    public DbSet<ZakaatCalculation> ZakaatCalculations => Set<ZakaatCalculation>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Sale>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.TotalAmount)
                  .HasPrecision(18, 2);
        });

        modelBuilder.Entity<SaleItem>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.UnitPrice)
                  .HasPrecision(18, 2);
            entity.Property(e => e.Subtotal)
                  .HasPrecision(18, 2);
        });

        modelBuilder.Entity<ZakaatCalculation>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.WealthAmount)
                  .HasPrecision(18, 2);
            entity.Property(e => e.PropertyValue)
                  .HasPrecision(18, 2);
            entity.Property(e => e.ZakaatAmount)
                  .HasPrecision(18, 2);
        });
    }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        foreach (var entry in ChangeTracker.Entries<BaseEntity>())
        {
            switch (entry.State)
            {
                case EntityState.Added:
                    entry.Entity.CreatedAt = DateTime.UtcNow;
                    break;
                case EntityState.Modified:
                    entry.Entity.UpdatedAt = DateTime.UtcNow;
                    break;
            }
        }

        return base.SaveChangesAsync(cancellationToken);
    }
}