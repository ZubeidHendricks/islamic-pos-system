using IslamicPOS.Domain.Inventory;
using IslamicPOS.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IslamicPOS.Infrastructure.Persistence.Configurations
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.Property(p => p.Name)
                .HasMaxLength(200)
                .IsRequired();

            builder.Property(p => p.SKU)
                .HasMaxLength(50)
                .IsRequired();

            builder.OwnsOne(p => p.Price, priceBuilder =>
            {
                priceBuilder.Property(m => m.Amount)
                    .HasColumnName("Price")
                    .HasPrecision(18, 2)
                    .IsRequired();

                priceBuilder.Property(m => m.Currency)
                    .HasColumnName("Currency")
                    .HasMaxLength(3)
                    .IsRequired();
            });

            builder.Property(p => p.HalalCertification)
                .HasMaxLength(100);

            builder.HasIndex(p => p.SKU)
                .IsUnique();
        }
    }
}