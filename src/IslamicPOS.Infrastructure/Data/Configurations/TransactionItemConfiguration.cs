using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using IslamicPOS.Core.Models;

namespace IslamicPOS.Infrastructure.Data.Configurations;

public class TransactionItemConfiguration : IEntityTypeConfiguration<TransactionItem>
{
    public void Configure(EntityTypeBuilder<TransactionItem> builder)
    {
        builder.HasKey(ti => ti.Id);

        builder.Property(ti => ti.ProductName)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(ti => ti.UnitPrice)
            .HasColumnType("decimal(18,2)")
            .IsRequired();

        builder.Property(ti => ti.Quantity)
            .IsRequired();

        builder.Property(ti => ti.Subtotal)
            .HasColumnType("decimal(18,2)")
            .IsRequired();

        builder.HasOne<Product>()
            .WithMany()
            .HasForeignKey(ti => ti.ProductId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}