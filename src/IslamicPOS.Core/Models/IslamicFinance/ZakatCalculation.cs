using IslamicPOS.Core.Models.Common;

namespace IslamicPOS.Core.Models.IslamicFinance;

public class ZakatCalculation : Entity
{
    public Money BusinessAssets { get; set; } = Money.Zero();
    public Money Cash { get; set; } = Money.Zero();
    public Money Investments { get; set; } = Money.Zero();
    public Money Liabilities { get; set; } = Money.Zero();
    public DateTime CalculationDate { get; set; }
    public decimal ZakatRate { get; set; } = 0.025m; // 2.5%

    public Money CalculateZakat()
    {
        var totalWealth = BusinessAssets.Amount + Cash.Amount + Investments.Amount - Liabilities.Amount;
        var zakatAmount = totalWealth * ZakatRate;
        return new Money(zakatAmount, BusinessAssets.Currency);
    }
}