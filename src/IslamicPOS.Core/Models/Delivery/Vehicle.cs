namespace IslamicPOS.Core.Models.Delivery;

public class Vehicle : AuditableEntity
{
    public string RegistrationNumber { get; set; } = string.Empty;
    public string Model { get; set; } = string.Empty;
    public decimal Capacity { get; set; }
    public string Status { get; set; } = string.Empty;
    public DateTime? LastMaintenance { get; set; }
    public DateTime? NextMaintenanceDue { get; set; }
}
