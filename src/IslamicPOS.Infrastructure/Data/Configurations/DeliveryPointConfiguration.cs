namespace IslamicPOS.Infrastructure.Data.Configurations;

public class DeliveryPointConfiguration : IEntityTypeConfiguration<DeliveryPoint>
{
    public void Configure(EntityTypeBuilder<DeliveryPoint> builder)
    {
        builder.HasKey(e => e.Id);

        builder.Property(e => e.Name)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(e => e.Address)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(e => e.ContactPerson)
            .HasMaxLength(100);

        builder.Property(e => e.ContactPhone)
            .HasMaxLength(20);

        builder.Property(e => e.SpecialInstructions)
            .HasMaxLength(500);

        builder.OwnsOne(e => e.DeliveryWindow, w =>
        {
            w.Property(p => p.Start).HasColumnName("DeliveryWindowStart");
            w.Property(p => p.End).HasColumnName("DeliveryWindowEnd");
        });
    }
}