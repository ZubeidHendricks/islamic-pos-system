namespace IslamicPOS.Core.Models.Reports;

public class ProfitReport
{
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public decimal TotalRevenue { get; set; }
    public decimal TotalCosts { get; set; }
    public decimal GrossProfit { get; set; }
    public decimal NetProfit { get; set; }
    public decimal ProfitMargin { get; set; }
    public List<string> Categories { get; set; } = new();
    public List<decimal> CategoryProfits { get; set; } = new();
}