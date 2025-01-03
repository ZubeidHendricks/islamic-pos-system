using IslamicPOS.Core.Models.Financial;
using IslamicPOS.Core.Models.IslamicFinance;
using Microsoft.EntityFrameworkCore;

namespace IslamicPOS.Infrastructure.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) {}

    public DbSet<Partner> Partners { get; set; }
    public DbSet<ProfitSharing> ProfitSharings { get; set; }
    public DbSet<ProfitDistributionDetail> ProfitDistributionDetails { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ProfitSharing>()
            .HasMany(p => p.DistributionDetails)
            .WithOne(d => d.ProfitSharing)
            .HasForeignKey(d => d.ProfitSharingId);

        modelBuilder.Entity<Partner>()
            .Property(p => p.SharePercentage)
            .HasPrecision(18, 4);

        modelBuilder.Entity<ProfitDistributionDetail>()
            .Property(d => d.Amount)
            .HasPrecision(18, 4);

        modelBuilder.Entity<ProfitDistributionDetail>()
            .Property(d => d.Percentage)
            .HasPrecision(18, 4);
    }
}