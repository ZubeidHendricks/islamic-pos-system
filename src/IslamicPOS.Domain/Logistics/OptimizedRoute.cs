using IslamicPOS.Domain.Common;

namespace IslamicPOS.Domain.Logistics;

public class OptimizedRoute : Entity
{
    public string RouteId { get; set; } = string.Empty;
    public Vehicle Vehicle { get; set; } = null!;
    public List<string> Destinations { get; set; } = new();
    public decimal TotalDistance { get; set; }
    public TimeSpan EstimatedTime { get; set; }
}