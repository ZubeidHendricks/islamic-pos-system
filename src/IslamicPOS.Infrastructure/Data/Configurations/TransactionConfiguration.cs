using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using IslamicPOS.Core.Models;

namespace IslamicPOS.Infrastructure.Data.Configurations;

public class TransactionConfiguration : IEntityTypeConfiguration<Transaction>
{
    public void Configure(EntityTypeBuilder<Transaction> builder)
    {
        builder.HasKey(t => t.Id);

        builder.Property(t => t.Timestamp)
            .IsRequired();

        builder.Property(t => t.TotalAmount)
            .HasColumnType("decimal(18,2)")
            .IsRequired();

        builder.Property(t => t.PaymentMethod)
            .HasMaxLength(50)
            .IsRequired();

        builder.Property(t => t.Status)
            .HasMaxLength(50)
            .IsRequired();

        builder.Property(t => t.Notes)
            .HasMaxLength(500);

        builder.Property(t => t.CreatedBy)
            .HasMaxLength(100)
            .IsRequired();

        builder.HasMany(t => t.Items)
            .WithOne()
            .HasForeignKey(ti => ti.TransactionId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}