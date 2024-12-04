namespace IslamicPOS.Core.Models.IslamicFinance;

public class MudarabahResult
{
    public decimal Capital { get; set; }
    public decimal TotalProfit { get; set; }
    public decimal RabAlMalShare { get; set; } // Capital provider's share percentage
    public decimal MudaribShare { get; set; } // Manager's share percentage
    public decimal RabAlMalAmount { get; set; } // Capital provider's profit amount
    public decimal MudaribAmount { get; set; } // Manager's profit amount

    public bool IsLoss => TotalProfit < 0;
    public decimal ReturnOnInvestment => Capital > 0 ? (TotalProfit / Capital) * 100 : 0;
}