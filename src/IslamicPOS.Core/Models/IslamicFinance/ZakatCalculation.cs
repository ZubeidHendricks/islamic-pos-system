namespace IslamicPOS.Core.Models.IslamicFinance;

public class ZakatCalculation : Entity
{
    public decimal BusinessAssets { get; private set; }
    public decimal CashAndEquivalents { get; private set; }
    public decimal Inventory { get; private set; }
    public decimal AccountsReceivable { get; private set; }
    public decimal Liabilities { get; private set; }
    public decimal TotalZakatableAmount { get; private set; }
    public decimal ZakatPayable { get; private set; }
    public DateTime CalculationDate { get; private set; }

    public ZakatCalculation(
        decimal businessAssets,
        decimal cashAndEquivalents,
        decimal inventory,
        decimal accountsReceivable,
        decimal liabilities,
        DateTime calculationDate)
    {
        BusinessAssets = businessAssets;
        CashAndEquivalents = cashAndEquivalents;
        Inventory = inventory;
        AccountsReceivable = accountsReceivable;
        Liabilities = liabilities;
        CalculationDate = calculationDate;

        CalculateZakat();
    }

    private void CalculateZakat()
    {
        TotalZakatableAmount = BusinessAssets + CashAndEquivalents + Inventory + AccountsReceivable - Liabilities;
        ZakatPayable = Math.Max(0, TotalZakatableAmount * 0.025m); // 2.5% is the standard zakat rate
    }
}