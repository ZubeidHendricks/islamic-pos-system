namespace IslamicPOS.Core.Models.Delivery;

public class OptimizedRoute : BaseEntity
{
    public List<DeliveryPoint> Points { get; set; } = new();
    public Vehicle? Vehicle { get; set; }
    public double TotalDistance { get; set; }
    public TimeSpan EstimatedDuration { get; set; }
    public List<DeliveryOrder> Orders { get; set; } = new();
}
