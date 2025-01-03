namespace IslamicPOS.Core.DTOs;

public class ProfitDistributionDetailDto
{
    public Guid Id { get; set; }
    public Guid PartnerId { get; set; }
    public string PartnerName { get; set; } = string.Empty;
    public decimal Amount { get; set; }
    public decimal Percentage { get; set; }
    public bool IsPaid { get; set; }
    public DateTime? PaymentDate { get; set; }
    public string PaymentReference { get; set; } = string.Empty;
    public string Notes { get; set; } = string.Empty;
}