using IslamicPOS.Domain.Common;

namespace IslamicPOS.Domain.Finance;

public class IslamicFinanceOptions
{
    public Money MinimumZakatThreshold { get; set; } = Money.Zero();
    public bool RequireHalalCertification { get; set; } = true;
    public string DefaultCurrency { get; set; } = "USD";
    public decimal DefaultProfitSharingRatio { get; set; } = 0.5m;

    public Money CalculateMinimumInvestment(Money baseAmount)
    {
        return new Money(baseAmount.Amount * 0.1m, baseAmount.Currency); // 10% of base amount
    }
}
