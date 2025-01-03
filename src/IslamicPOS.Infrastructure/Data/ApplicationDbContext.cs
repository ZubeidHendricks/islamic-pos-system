using IslamicPOS.Core.Models.Financial;
using IslamicPOS.Core.Models.IslamicFinance;
using Microsoft.EntityFrameworkCore;

namespace IslamicPOS.Infrastructure.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<Partner> Partners { get; set; }
    public DbSet<ProfitSharing> ProfitSharings { get; set; }
    public DbSet<ProfitDistributionDetail> ProfitDistributionDetails { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
    }
}