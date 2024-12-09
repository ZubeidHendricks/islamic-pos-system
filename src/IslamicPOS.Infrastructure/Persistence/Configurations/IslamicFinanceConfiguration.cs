using IslamicPOS.Domain.Finance;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IslamicPOS.Infrastructure.Persistence.Configurations
{
    public class IslamicFinanceOptionsConfiguration : IEntityTypeConfiguration<IslamicFinanceOptions>
    {
        public void Configure(EntityTypeBuilder<IslamicFinanceOptions> builder)
        {
            builder.Property(e => e.ZakaatRate)
                .HasPrecision(5, 4);

            builder.Property(e => e.DefaultProfitSharingRatio)
                .HasPrecision(5, 4);

            builder.OwnsOne(e => e.NisabThreshold, nb =>
            {
                nb.Property(p => p.Amount)
                    .HasColumnName("NisabThresholdAmount")
                    .HasPrecision(18, 2);

                nb.Property(p => p.Currency)
                    .HasColumnName("NisabThresholdCurrency")
                    .HasMaxLength(3);
            });
        }
    }
}