namespace IslamicPOS.Core.Models.IslamicFinance;

public class MudarabahResult : Entity
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

public class MusharakaResult : Entity
{
    public decimal TotalInvestment { get; set; }
    public decimal Partner1Investment { get; set; }
    public decimal Partner2Investment { get; set; }
    public decimal ProfitSharingRatio1 { get; set; }
    public decimal ProfitSharingRatio2 { get; set; }
    public decimal Partner1Share { get; set; }
    public decimal Partner2Share { get; set; }
    public DateTime CalculationDate { get; set; }
}

public class ZakaatCalculation : Entity
{
    public decimal BusinessAssets { get; set; }
    public decimal CashAndEquivalents { get; set; }
    public decimal Inventory { get; set; }
    public decimal AccountsReceivable { get; set; }
    public decimal Liabilities { get; set; }
    public decimal TotalZakaableAmount { get; set; }
    public decimal ZakaatPayable { get; set; }
    public DateTime CalculationDate { get; set; }
}

public class ZakaatInput
{
    public decimal GoldValue { get; set; }
    public decimal SilverValue { get; set; }
    public decimal CashValue { get; set; }
    public decimal BusinessAssets { get; set; }
    public decimal Receivables { get; set; }
    public decimal Debts { get; set; }
    public decimal OtherAssets { get; set; }
}