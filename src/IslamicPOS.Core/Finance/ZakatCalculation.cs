using IslamicPOS.Core.Common;

namespace IslamicPOS.Core.Finance;

public class ZakatCalculation : Entity
{
    public Money BusinessAssets { get; private set; }
    public Money CashAndEquivalents { get; private set; }
    public Money TotalZakatableAmount { get; private set; }
    public Money ZakatPayable { get; private set; }
    public DateTime CalculationDate { get; private set; }
    
    private ZakatCalculation(
        Money businessAssets,
        Money cashAndEquivalents,
        DateTime calculationDate)
    {
        BusinessAssets = businessAssets;
        CashAndEquivalents = cashAndEquivalents;
        CalculationDate = calculationDate;
        CalculateZakat();
    }

    public static ZakatCalculation Create(
        Money businessAssets,
        Money cashAndEquivalents)
    {
        return new ZakatCalculation(
            businessAssets,
            cashAndEquivalents,
            DateTime.UtcNow);
    }

    private void CalculateZakat()
    {
        var totalAmount = BusinessAssets.Add(CashAndEquivalents);
        TotalZakatableAmount = totalAmount;
        ZakatPayable = Money.Create(totalAmount.Amount * 0.025m, totalAmount.Currency); // 2.5% standard zakat rate
    }
}