namespace IslamicPOS.Core.Models.Logistics;

public class DeliveryVehicle
{
    public int Id { get; set; }
    public string RegistrationNumber { get; set; } = string.Empty;
    public string VehicleType { get; set; } = string.Empty;
    public decimal LoadCapacityKg { get; set; }
    public decimal VolumeCapacityM3 { get; set; }
    public bool HasRefrigeration { get; set; }
    public bool HasTemperatureMonitoring { get; set; }
    public string Status { get; set; } = string.Empty;
    public string CurrentLocation { get; set; } = string.Empty;
    public double? CurrentLatitude { get; set; }
    public double? CurrentLongitude { get; set; }
    public DateTime LastMaintenanceDate { get; set; }
    public DateTime NextMaintenanceDate { get; set; }
    public int TotalTrips { get; set; }
    public decimal TotalDistanceCovered { get; set; }
    public bool IsHalalCertified { get; set; }
    public string HalalCertificationNumber { get; set; } = string.Empty;
    public DateTime? HalalCertificationExpiry { get; set; }
}