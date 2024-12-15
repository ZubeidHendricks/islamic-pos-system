using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using IslamicPOS.Application.Common.Interfaces;
using IslamicPOS.Domain.Common;
using IslamicPOS.Domain.Inventory;
using IslamicPOS.Domain.Sales;
using IslamicPOS.Domain.Finance;
using IslamicPOS.Domain.Identity;
using System.Reflection;

namespace IslamicPOS.Infrastructure.Persistence;

public class ApplicationDbContext : IdentityDbContext<ApplicationUser>, IApplicationDbContext
{
    public DbSet<Product> Products { get; set; }
    public DbSet<Sale> Sales { get; set; }
    public DbSet<Transaction> Transactions { get; set; }
    public DbSet<MudarabahContract> MudarabahContracts { get; set; }
    public DbSet<ZakaatCalculation> ZakaatCalculations { get; set; }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Apply configurations from the current assembly
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}