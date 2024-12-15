using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using IslamicPOS.Domain.Logistics;

namespace IslamicPOS.Infrastructure.Data.Configurations;

public class OptimizedRouteConfiguration : IEntityTypeConfiguration<OptimizedRoute>
{
    public void Configure(EntityTypeBuilder<OptimizedRoute> builder)
    {
        builder.HasKey(r => r.Id);
        builder.Property(r => r.RouteId).IsRequired();
        builder.HasOne<Vehicle>().WithMany();
        builder.Property(r => r.TotalDistance).HasPrecision(18, 2);
    }
}