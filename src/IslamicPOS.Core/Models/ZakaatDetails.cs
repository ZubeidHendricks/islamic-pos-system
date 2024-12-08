namespace IslamicPOS.Core.Models;

public class ZakaatDetails
{
    public decimal Cash { get; set; }
    public decimal BankAccounts { get; set; }
    public decimal Gold { get; set; }
    public decimal Silver { get; set; }
    public decimal Investments { get; set; }
    public decimal BusinessInventory { get; set; }
    public decimal AccountsReceivable { get; set; }
    public decimal OtherAssets { get; set; }
    public decimal TotalLiabilities { get; set; }

    public decimal TotalAssets =>
        Cash +
        BankAccounts +
        Gold +
        Silver +
        Investments +
        BusinessInventory +
        AccountsReceivable +
        OtherAssets;

    public decimal NetWorth => TotalAssets - TotalLiabilities;
}