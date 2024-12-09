using IslamicPOS.Application.Common.Interfaces;
using IslamicPOS.Domain.Finance;
using IslamicPOS.Domain.Inventory;
using Microsoft.EntityFrameworkCore;

namespace IslamicPOS.Infrastructure.Persistence
{
    public class ApplicationDbContext : DbContext, IApplicationDbContext
    {
        public DbSet<Product> Products => Set<Product>();
        public DbSet<ProductCategory> Categories => Set<ProductCategory>();
        public DbSet<MudarabahContract> MudarabahContracts => Set<MudarabahContract>();
        public DbSet<ZakaatCalculation> ZakaatCalculations => Set<ZakaatCalculation>();

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);

            base.OnModelCreating(modelBuilder);
        }
    }
}