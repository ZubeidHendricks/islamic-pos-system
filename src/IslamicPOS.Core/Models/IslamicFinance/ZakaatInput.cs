namespace IslamicPOS.Core.Models.IslamicFinance;

public class ZakaatInput
{
    public decimal GoldValue { get; set; }
    public decimal SilverValue { get; set; }
    public decimal CashValue { get; set; }
    public decimal BusinessAssets { get; set; }
    public decimal Receivables { get; set; }
    public decimal Debts { get; set; }
    public decimal OtherAssets { get; set; }
    public string? Notes { get; set; }
}
