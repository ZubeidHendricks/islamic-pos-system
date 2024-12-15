using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using IslamicPOS.Domain.Common;

namespace IslamicPOS.Infrastructure.Data.Configurations;

public class TransactionItemConfiguration : IEntityTypeConfiguration<TransactionItem>
{
    public void Configure(EntityTypeBuilder<TransactionItem> builder)
    {
        builder.HasKey(e => e.Id);

        builder.Property(e => e.Quantity)
            .IsRequired();

        builder.Property(e => e.UnitPrice)
            .HasPrecision(18, 2);
    }
}