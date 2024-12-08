namespace IslamicPOS.Core.Models.IslamicFinance;

public class MudarabahResult
{
    public decimal InvestmentAmount { get; set; }
    public decimal ProfitShare { get; set; }
    public decimal InvestorShare { get; set; }
    public decimal MudaribShare { get; set; }
    public decimal TotalReturn { get; set; }
    public decimal ROI { get; set; }
    public DateTime CalculationDate { get; set; }
    public string? Notes { get; set; }
}