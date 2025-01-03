using IslamicPOS.Core.Models.Financial;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IslamicPOS.Infrastructure.Data.Configurations;

public class PartnerConfiguration : IEntityTypeConfiguration<Partner>
{
    public void Configure(EntityTypeBuilder<Partner> builder)
    {
        builder.Property(p => p.Name)
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(p => p.ContactNumber)
            .HasMaxLength(20);

        builder.Property(p => p.Email)
            .HasMaxLength(100);

        builder.Property(p => p.SharePercentage)
            .HasPrecision(18, 4);

        builder.Property(p => p.Notes)
            .HasMaxLength(500);
    }
}