using IslamicPOS.Core.Models.Common;

namespace IslamicPOS.Core.Models.Logistics;

public class OptimizedRoute : Entity
{
    public Guid VehicleId { get; set; }
    public Vehicle Vehicle { get; set; } = null!;
    public List<DeliveryPoint> Points { get; set; } = new();
    public double TotalDistance { get; set; }
    public TimeSpan EstimatedDuration { get; set; }
    public List<DeliveryOrder> Orders { get; set; } = new();
}