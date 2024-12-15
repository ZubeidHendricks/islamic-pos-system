using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using IslamicPOS.Domain.Logistics;

namespace IslamicPOS.Infrastructure.Data.Configurations;

public class DeliveryPointConfiguration : IEntityTypeConfiguration<DeliveryPoint>
{
    public void Configure(EntityTypeBuilder<DeliveryPoint> builder)
    {
        builder.HasKey(e => e.Id);

        builder.Property(e => e.Name)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(e => e.Address)
            .IsRequired()
            .HasMaxLength(500);
    }
}