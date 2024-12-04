namespace IslamicPOS.Core.Models.Logistics;

public class Driver
{
    public string Id { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string LicenseNumber { get; set; } = string.Empty;
    public string LicenseType { get; set; } = string.Empty;
    public DateTime LicenseExpiry { get; set; }
    public string Phone { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Status { get; set; } = string.Empty;
    public bool IsAvailable { get; set; }
    public string CurrentLocation { get; set; } = string.Empty;
    public double? CurrentLatitude { get; set; }
    public double? CurrentLongitude { get; set; }
    public int CompletedDeliveries { get; set; }
    public int TotalHoursWorked { get; set; }
    public decimal SuccessRate { get; set; }
    public bool HasHalalCertification { get; set; }
    public DateTime? LastTrainingDate { get; set; }
    public List<string> Certifications { get; set; } = new();
}