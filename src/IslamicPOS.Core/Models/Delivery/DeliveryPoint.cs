namespace IslamicPOS.Core.Models.Delivery;

public class DeliveryPoint
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
    public double Latitude { get; set; }
    public double Longitude { get; set; }
    public string ContactPhone { get; set; } = string.Empty;
    public string? DeliveryInstructions { get; set; }
}
