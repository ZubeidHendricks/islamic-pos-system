namespace IslamicPOS.Core.Models.Delivery;

public class Vehicle
{
    public int Id { get; set; }
    public string RegistrationNumber { get; set; } = string.Empty;
    public string Model { get; set; } = string.Empty;
    public decimal LoadCapacity { get; set; }
    public string Status { get; set; } = string.Empty;
    public DateTime? LastMaintenanceDate { get; set; }
    public bool IsActive { get; set; }
}
