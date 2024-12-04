namespace IslamicPOS.Core.Models;

public class ZakaatInput
{
    public decimal Cash { get; set; }
    public decimal BankAccounts { get; set; }
    public decimal Gold { get; set; }
    public decimal Silver { get; set; }
    public decimal Investments { get; set; }
    public decimal BusinessInventory { get; set; }
    public decimal AccountsReceivable { get; set; }
    public decimal OtherAssets { get; set; }
    public decimal Liabilities { get; set; }
}