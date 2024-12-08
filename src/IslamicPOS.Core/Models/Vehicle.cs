namespace IslamicPOS.Core.Models;

public class Vehicle
{
    public int Id { get; set; }
    public string RegistrationNumber { get; set; } = string.Empty;
    public string Type { get; set; } = string.Empty;
    public decimal Capacity { get; set; }
    public bool IsHalalCertified { get; set; }
    public DateTime? HalalCertificationExpiry { get; set; }
    public bool IsActive { get; set; } = true;
}