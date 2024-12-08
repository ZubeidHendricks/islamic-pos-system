using IslamicPOS.Core.Models.Common;

namespace IslamicPOS.Core.Models.Logistics;

public class DeliveryPoint : Entity
{
    public string Name { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
    public double Latitude { get; set; }
    public double Longitude { get; set; }
    public string ContactName { get; set; } = string.Empty;
    public string ContactPhone { get; set; } = string.Empty;
    public string? Instructions { get; set; }
}