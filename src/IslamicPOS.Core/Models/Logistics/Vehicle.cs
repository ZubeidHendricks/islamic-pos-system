using IslamicPOS.Core.Models.Common;

namespace IslamicPOS.Core.Models.Logistics;

public class Vehicle : Entity
{
    public string RegistrationNumber { get; set; } = string.Empty;
    public string Model { get; set; } = string.Empty;
    public string Make { get; set; } = string.Empty;
    public decimal LoadCapacity { get; set; }
    public string Status { get; set; } = string.Empty;
    public DateTime? LastMaintenanceDate { get; set; }
    public DateTime? NextMaintenanceDate { get; set; }
}