using IslamicPOS.Core.Models.IslamicFinance;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IslamicPOS.Infrastructure.Data.Configurations;

public class ProfitDistributionDetailConfiguration : IEntityTypeConfiguration<ProfitDistributionDetail>
{
    public void Configure(EntityTypeBuilder<ProfitDistributionDetail> builder)
    {
        builder.Property(p => p.Amount)
            .HasPrecision(18, 4)
            .IsRequired();

        builder.Property(p => p.Percentage)
            .HasPrecision(18, 4)
            .IsRequired();

        builder.Property(p => p.PaymentReference)
            .HasMaxLength(100);

        builder.Property(p => p.Notes)
            .HasMaxLength(500);

        builder.HasOne(p => p.Partner)
            .WithMany()
            .HasForeignKey(p => p.PartnerId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}