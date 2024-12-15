using IslamicPOS.Domain.Common;

namespace IslamicPOS.Domain.Finance.Models;

public class ZakaatCalculation : EntityBase
{
    private Money _assets = Money.Zero();
    private Money _liabilities = Money.Zero();
    private Money _businessAssets = Money.Zero();
    private Money _investments = Money.Zero();

    public Money Assets => _assets;
    public Money Liabilities => _liabilities;
    public Money BusinessAssets => _businessAssets;
    public Money Investments => _investments;
    public DateTime CalculationDate { get; private set; }
    public decimal ZakaatRate { get; private set; } = 0.025m; // 2.5%

    protected ZakaatCalculation() { } // For EF Core

    public ZakaatCalculation(
        Money assets,
        Money liabilities,
        Money businessAssets,
        Money investments)
    {
        _assets = assets;
        _liabilities = liabilities;
        _businessAssets = businessAssets;
        _investments = investments;
        CalculationDate = DateTime.UtcNow;
    }

    public Money CalculateZakaat()
    {
        var totalAssets = _assets.Amount + _businessAssets.Amount + _investments.Amount;
        var netWorth = totalAssets - _liabilities.Amount;
        var zakaatAmount = netWorth * ZakaatRate;
        
        return new Money(zakaatAmount, _assets.Currency);
    }
}
