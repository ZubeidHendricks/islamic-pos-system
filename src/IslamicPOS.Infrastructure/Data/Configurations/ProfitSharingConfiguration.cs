using IslamicPOS.Core.Models.IslamicFinance;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IslamicPOS.Infrastructure.Data.Configurations;

public class ProfitSharingConfiguration : IEntityTypeConfiguration<ProfitSharing>
{
    public void Configure(EntityTypeBuilder<ProfitSharing> builder)
    {
        builder.Property(p => p.TotalAmount)
            .HasPrecision(18, 4)
            .IsRequired();

        builder.Property(p => p.Description)
            .HasMaxLength(500);

        builder.Property(p => p.Period)
            .HasMaxLength(10)
            .IsRequired();

        builder.HasMany(p => p.DistributionDetails)
            .WithOne(d => d.ProfitSharing)
            .HasForeignKey(d => d.ProfitSharingId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}