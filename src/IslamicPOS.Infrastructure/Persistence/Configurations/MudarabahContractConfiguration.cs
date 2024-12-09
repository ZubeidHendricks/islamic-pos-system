using IslamicPOS.Domain.Finance;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IslamicPOS.Infrastructure.Persistence.Configurations
{
    public class MudarabahContractConfiguration : IEntityTypeConfiguration<MudarabahContract>
    {
        public void Configure(EntityTypeBuilder<MudarabahContract> builder)
        {
            builder.Property(c => c.ContractNumber)
                .HasMaxLength(50)
                .IsRequired();

            builder.OwnsOne(c => c.InvestedCapital, moneyBuilder =>
            {
                moneyBuilder.Property(m => m.Amount)
                    .HasColumnName("InvestedAmount")
                    .HasPrecision(18, 2)
                    .IsRequired();

                moneyBuilder.Property(m => m.Currency)
                    .HasColumnName("Currency")
                    .HasMaxLength(3)
                    .IsRequired();
            });

            builder.Property(c => c.ProfitSharingRatio)
                .HasPrecision(5, 4)
                .IsRequired();

            builder.HasMany(c => c.ProfitDistributions)
                .WithOne()
                .HasForeignKey("MudarabahContractId")
                .OnDelete(DeleteBehavior.Cascade);

            builder.Property(c => c.Status)
                .HasConversion<string>()
                .HasMaxLength(20);
        }
    }
}