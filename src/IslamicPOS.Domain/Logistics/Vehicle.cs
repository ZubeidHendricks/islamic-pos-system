namespace IslamicPOS.Domain.Logistics;

public class Vehicle
{
    public int Id { get; set; }
    public string VehicleType { get; set; } = string.Empty;
    public string LicensePlate { get; set; } = string.Empty;
    public int Capacity { get; set; }
}