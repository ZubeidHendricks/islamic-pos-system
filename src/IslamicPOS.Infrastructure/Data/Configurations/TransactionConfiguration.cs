using IslamicPOS.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IslamicPOS.Infrastructure.Data.Configurations;

public class TransactionConfiguration : IEntityTypeConfiguration<Transaction>
{
    public void Configure(EntityTypeBuilder<Transaction> builder)
    {
        builder.HasKey(t => t.Id);
        builder.Property(t => t.TransactionAmount).IsRequired();
        builder.Property(t => t.TransactionType).IsRequired();
        builder.Property(t => t.TransactionDescription).HasMaxLength(500);
    }
}
