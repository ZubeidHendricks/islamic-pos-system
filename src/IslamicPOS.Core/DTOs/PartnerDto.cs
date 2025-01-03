namespace IslamicPOS.Core.DTOs;

public class PartnerDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string ContactNumber { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public decimal SharePercentage { get; set; }
    public bool IsActive { get; set; }
    public string Notes { get; set; } = string.Empty;
}