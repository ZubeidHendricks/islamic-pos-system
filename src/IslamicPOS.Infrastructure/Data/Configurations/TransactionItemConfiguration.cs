using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using IslamicPOS.Core.Models;

namespace IslamicPOS.Infrastructure.Data.Configurations;

public class TransactionItemConfiguration : IEntityTypeConfiguration<TransactionItem>
{
    public void Configure(EntityTypeBuilder<TransactionItem> builder)
    {
        builder.Property(ti => ti.ProductName)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(ti => ti.UnitPrice)
            .HasColumnType("decimal(18,2)")
            .IsRequired();

        builder.Property(ti => ti.Quantity)
            .IsRequired();
    }
}