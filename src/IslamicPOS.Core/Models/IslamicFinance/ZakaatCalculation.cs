namespace IslamicPOS.Core.Models.IslamicFinance;

public class ZakaatCalculation
{
    public decimal BusinessAssets { get; set; }
    public decimal CashAndEquivalents { get; set; }
    public decimal Inventory { get; set; }
    public decimal AccountsReceivable { get; set; }
    public decimal Liabilities { get; set; }
    public decimal TotalZakaableAmount { get; set; }
    public decimal ZakaatPayable { get; set; }
    public DateTime CalculationDate { get; set; }
    public string? Notes { get; set; }
}
