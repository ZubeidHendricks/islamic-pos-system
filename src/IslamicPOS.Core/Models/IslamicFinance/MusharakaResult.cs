namespace IslamicPOS.Core.Models.IslamicFinance;

public class MusharakaResult
{
    public decimal TotalCapital { get; set; }
    public decimal TotalProfit { get; set; }
    public List<PartnerShare> Partners { get; set; } = new();

    public bool IsLoss => TotalProfit < 0;
    public decimal ReturnOnInvestment => TotalCapital > 0 ? (TotalProfit / TotalCapital) * 100 : 0;
}

public class PartnerShare
{
    public string PartnerName { get; set; } = string.Empty;
    public decimal CapitalContribution { get; set; }
    public decimal CapitalSharePercentage { get; set; }
    public decimal ProfitSharePercentage { get; set; }
    public decimal ProfitAmount { get; set; }
}