using IslamicPOS.Core.Common;

namespace IslamicPOS.Core.Finance;

public class ZakaatCalculation : Entity
{
    public Money BusinessAssets { get; private set; }
    public Money CashAndEquivalents { get; private set; }
    public Money TotalZakaatableAmount { get; private set; }
    public Money ZakaatPayable { get; private set; }
    public DateTime CalculationDate { get; private set; }
    
    private ZakaatCalculation(
        Money businessAssets,
        Money cashAndEquivalents,
        DateTime calculationDate)
    {
        BusinessAssets = businessAssets;
        CashAndEquivalents = cashAndEquivalents;
        CalculationDate = calculationDate;
        CalculateZakaat();
    }

    public static ZakaatCalculation Create(
        Money businessAssets,
        Money cashAndEquivalents)
    {
        return new ZakaatCalculation(
            businessAssets,
            cashAndEquivalents,
            DateTime.UtcNow);
    }

    private void CalculateZakaat()
    {
        var totalAmount = BusinessAssets.Add(CashAndEquivalents);
        TotalZakaatableAmount = totalAmount;
        ZakaatPayable = Money.Create(totalAmount.Amount * 0.025m, totalAmount.Currency); // 2.5% standard zakat rate
    }
}