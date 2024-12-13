using Microsoft.EntityFrameworkCore;
using IslamicPOS.Infrastructure.Data.Models;

namespace IslamicPOS.Infrastructure.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<PrinterConfigurationEntity> PrinterConfigurations { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<PrinterConfigurationEntity>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.PrinterName).IsRequired().HasMaxLength(200);
                entity.Property(e => e.PaperSize).HasMaxLength(50);
                entity.Property(e => e.HeaderText).HasMaxLength(500);
                entity.Property(e => e.FooterText).HasMaxLength(500);

                // Add index for faster lookups
                entity.HasIndex(e => e.PrinterName).IsUnique();
                entity.HasIndex(e => e.IsDefault);
            });
        }
    }
}