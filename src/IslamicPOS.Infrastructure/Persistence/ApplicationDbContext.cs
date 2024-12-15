using Microsoft.EntityFrameworkCore;
using IslamicPOS.Application.Common.Interfaces;
using IslamicPOS.Domain.Common;
using IslamicPOS.Domain.Inventory;
using IslamicPOS.Domain.Sales;
using IslamicPOS.Domain.Finance;

namespace IslamicPOS.Infrastructure.Persistence;

public class ApplicationDbContext : DbContext, IApplicationDbContext
{
    public DbSet<Product> Products { get; set; }
    public DbSet<Sale> Sales { get; set; }
    public DbSet<Transaction> Transactions { get; set; }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        // Add any additional configuration
    }
}