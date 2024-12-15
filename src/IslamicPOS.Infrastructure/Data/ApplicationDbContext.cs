using IslamicPOS.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace IslamicPOS.Infrastructure.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        modelBuilder.Entity<ZakaatCalculation>(entity =>
        {
            entity.HasKey(z => z.Id);
            entity.Property(z => z.WealthAmount).IsRequired();
            entity.Property(z => z.PropertyValue).IsRequired();
            // Add other Zakaat-specific configurations
        });
    }

    public DbSet<Product> Products { get; set; } = null!;
    public DbSet<Category> Categories { get; set; } = null!;
    public DbSet<Sale> Sales { get; set; } = null!;
    public DbSet<Partner> Partners { get; set; } = null!;
    public DbSet<Transaction> Transactions { get; set; } = null!;
    public DbSet<ZakaatCalculation> ZakaatCalculations { get; set; } = null!;
}
