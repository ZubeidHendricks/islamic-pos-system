namespace IslamicPOS.Core.Models.Delivery;

public class OptimizedRoute
{
    public int Id { get; set; }
    public List<DeliveryPoint> DeliveryPoints { get; set; } = new();
    public Vehicle? Vehicle { get; set; }
    public double TotalDistance { get; set; }
    public TimeSpan EstimatedDuration { get; set; }
    public List<DeliveryOrder> Orders { get; set; } = new();
}
