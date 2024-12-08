namespace IslamicPOS.Core.Models.IslamicFinance;

public class MusharakaResult
{
    public decimal TotalInvestment { get; set; }
    public decimal Partner1Investment { get; set; }
    public decimal Partner2Investment { get; set; }
    public decimal ProfitSharingRatio1 { get; set; }
    public decimal ProfitSharingRatio2 { get; set; }
    public decimal Partner1Share { get; set; }
    public decimal Partner2Share { get; set; }
    public DateTime CalculationDate { get; set; }
    public string? Notes { get; set; }
}