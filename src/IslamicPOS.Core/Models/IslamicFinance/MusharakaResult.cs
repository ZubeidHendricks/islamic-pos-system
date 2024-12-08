namespace IslamicPOS.Core.Models.IslamicFinance;

public class MusharakaResult
{
    public decimal TotalCapital { get; set; }
    public decimal Partner1Capital { get; set; }
    public decimal Partner2Capital { get; set; }
    public decimal ProfitShareRatio1 { get; set; }
    public decimal ProfitShareRatio2 { get; set; }
    public decimal TotalProfit { get; set; }
    public decimal Partner1Share { get; set; }
    public decimal Partner2Share { get; set; }
    public DateTime CalculationDate { get; set; }
    public string? Notes { get; set; }
}
