namespace IslamicPOS.Core.Models.Reports;

public class ZakaahReport
{
    public DateTime CalculationDate { get; set; }
    public decimal TotalZakaableAssets { get; set; }
    public decimal ZakaahAmount { get; set; }
    public decimal BusinessAssets { get; set; }
    public decimal CashAndEquivalents { get; set; }
    public decimal Inventory { get; set; }
    public decimal Receivables { get; set; }
    public decimal Deductions { get; set; }
    public string? Notes { get; set; }
}