namespace IslamicPOS.Infrastructure.Data.Configurations;

public class OptimizedRouteConfiguration : IEntityTypeConfiguration<OptimizedRoute>
{
    public void Configure(EntityTypeBuilder<OptimizedRoute> builder)
    {
        builder.HasKey(e => e.Id);

        builder.Property(e => e.Date)
            .IsRequired();

        builder.Property(e => e.Status)
            .IsRequired()
            .HasMaxLength(50);

        builder.Property(e => e.TotalDistance)
            .HasPrecision(10, 2);

        builder.Property(e => e.EstimatedDuration)
            .IsRequired();

        builder.HasOne(e => e.Vehicle)
            .WithMany()
            .HasForeignKey(e => e.VehicleId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasMany(e => e.Stops)
            .WithMany()
            .UsingEntity("OptimizedRouteStops",
                l => l.HasOne(typeof(DeliveryPoint)).WithMany().HasForeignKey("DeliveryPointId"),
                r => r.HasOne(typeof(OptimizedRoute)).WithMany().HasForeignKey("OptimizedRouteId"));
    }
}