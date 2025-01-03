namespace IslamicPOS.Core.DTOs;

public class ProfitSharingDto
{
    public Guid Id { get; set; }
    public decimal TotalAmount { get; set; }
    public DateTime DistributionDate { get; set; }
    public string Description { get; set; } = string.Empty;
    public string Period { get; set; } = string.Empty;
    public bool IsDistributed { get; set; }
    public DateTime? DistributedAt { get; set; }
    public List<ProfitDistributionDetailDto> DistributionDetails { get; set; } = new();
}