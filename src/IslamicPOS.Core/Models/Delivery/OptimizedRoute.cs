using IslamicPOS.Core.Models.Common;

namespace IslamicPOS.Core.Models.Delivery;

public class OptimizedRoute : Entity
{
    public Vehicle Vehicle { get; set; } = null!;
    public List<DeliveryPoint> Points { get; set; } = new();
    public double TotalDistance { get; set; }
    public TimeSpan EstimatedDuration { get; set; }
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }
    public string Status { get; set; } = string.Empty;
}