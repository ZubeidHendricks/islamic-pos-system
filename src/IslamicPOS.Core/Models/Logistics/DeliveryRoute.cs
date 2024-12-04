namespace IslamicPOS.Core.Models.Logistics;

public class DeliveryRoute
{
    public int Id { get; set; }
    public string RouteName { get; set; } = string.Empty;
    public int VehicleId { get; set; }
    public string DriverId { get; set; } = string.Empty;
    public DateTime StartTime { get; set; }
    public DateTime EstimatedEndTime { get; set; }
    public string Status { get; set; } = string.Empty;
    public decimal TotalDistanceKm { get; set; }
    public int TotalStops { get; set; }
    public decimal TotalLoadKg { get; set; }
    public decimal TotalVolumeM3 { get; set; }
    public bool RequiresRefrigeration { get; set; }
    public decimal? RequiredTemperature { get; set; }
    public List<DeliveryStop> Stops { get; set; } = new();
    
    // Navigation properties
    public DeliveryVehicle Vehicle { get; set; } = null!;
    public Driver Driver { get; set; } = null!;
}