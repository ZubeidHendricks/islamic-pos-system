using IslamicPOS.Domain.Logistics.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IslamicPOS.Infrastructure.Data.Configurations
{
    public class RouteConfiguration : IEntityTypeConfiguration<OptimizedRoute>
    {
        public void Configure(EntityTypeBuilder<OptimizedRoute> builder)
        {
            builder.HasKey(r => r.RouteId);
            builder.Property(r => r.TotalDistance).IsRequired();
            builder.Property(r => r.EstimatedDuration).IsRequired();
            builder.Property(r => r.Status).IsRequired();
            builder.Property(r => r.RecommendedVehicleType).IsRequired();
            builder.HasMany(r => r.Waypoints).WithOne();
        }
    }

    public class DeliveryPointConfiguration : IEntityTypeConfiguration<DeliveryPoint>
    {
        public void Configure(EntityTypeBuilder<DeliveryPoint> builder)
        {
            builder.HasKey(d => d.Id);
            builder.Property(d => d.Name).IsRequired();
            builder.Property(d => d.Address).IsRequired();
            builder.Property(d => d.Latitude).IsRequired();
            builder.Property(d => d.Longitude).IsRequired();
            builder.OwnsOne(d => d.DeliveryWindow);
        }
    }
}
