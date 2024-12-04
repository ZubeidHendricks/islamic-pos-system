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

        builder.Property(t => t.Status)
            .IsRequired()
            .HasConversion<string>();

        builder.Property(t => t.PaymentMethod)
            .IsRequired()
            .HasConversion<string>();

        builder.HasMany(t => t.Items)
            .WithOne()
            .HasForeignKey("TransactionId")
            .OnDelete(DeleteBehavior.Cascade);
    }
}