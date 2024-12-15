namespace IslamicPOS.Domain.Logistics;

public class DeliveryPoint
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public required string Address { get; set; }
    public double Latitude { get; set; }
    public double Longitude { get; set; }
}