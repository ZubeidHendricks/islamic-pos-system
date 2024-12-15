using IslamicPOS.Core.Common;
using IslamicPOS.Core.Models.Common;

namespace IslamicPOS.Core.Models.IslamicFinance;

public class ProfitSharing : Entity
{
    public Money TotalProfit { get; set; } = Money.Zero();
    public decimal InvestorShare { get; set; } // Percentage
    public decimal BusinessShare { get; set; } // Percentage
    public Money Reserves { get; set; } = Money.Zero();
    public DateTime PeriodStart { get; set; }
    public DateTime PeriodEnd { get; set; }
    public string CalculationMethod { get; set; } = string.Empty; // Mudarabah or Musharakah

    public (Money investorAmount, Money businessAmount) CalculateShares()
    {
        var distributableAmount = TotalProfit.Amount - Reserves.Amount;
        var investorAmount = distributableAmount * (InvestorShare / 100m);
        var businessAmount = distributableAmount * (BusinessShare / 100m);

        return (new Money(investorAmount, TotalProfit.Currency),
                new Money(businessAmount, TotalProfit.Currency));
    }
}
