using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace IslamicPOS.Infrastructure.Persistence;

public class ApplicationDbContext : IdentityDbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<Product> Products { get; set; } = null!;
    public DbSet<Transaction> Transactions { get; set; } = null!;
    public DbSet<TransactionItem> TransactionItems { get; set; } = null!;
    public DbSet<Partner> Partners { get; set; } = null!;
    public DbSet<PaymentMethod> PaymentMethods { get; set; } = null!;
    public DbSet<ProfitSharing> ProfitSharings { get; set; } = null!;
    public DbSet<ProfitDistributionDetail> ProfitDistributionDetails { get; set; } = null!;
    public DbSet<ZakatCalculation> ZakatCalculations { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        // Configure soft delete filter
        foreach (var entityType in builder.Model.GetEntityTypes())
        {
            if (typeof(Core.Common.Entity).IsAssignableFrom(entityType.ClrType))
            {
                builder.Entity(entityType.ClrType).HasQueryFilter(e => !EF.Property<bool>(e, "IsDeleted"));
            }
        }
    }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        foreach (var entry in ChangeTracker.Entries<Core.Common.Entity>())
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