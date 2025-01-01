using IslamicPOS.Core.Common;

namespace IslamicPOS.Core.Models.IslamicFinance;

public class ZakatCalculation : Entity
{
    public DateTime CalculationDate { get; set; }
    public decimal TotalAssets { get; set; }
    public decimal TotalLiabilities { get; set; }
    public decimal NetWorth { get; set; }
    public decimal NisabThreshold { get; set; }
    public bool IsEligible { get; set; }
    public decimal ZakatAmount { get; set; }
    public string CalculationMethod { get; set; } = string.Empty;
    public string Notes { get; set; } = string.Empty;
    public bool IsPaid { get; set; }
    public DateTime? PaymentDate { get; set; }
}