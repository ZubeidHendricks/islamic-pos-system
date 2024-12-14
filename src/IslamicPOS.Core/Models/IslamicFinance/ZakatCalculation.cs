using IslamicPOS.Core.Models.Common;

namespace IslamicPOS.Core.Models.IslamicFinance
{
    public class ZakatCalculation
    {
        public Money Amount { get; private set; }
        public Money NisabThreshold { get; private set; }
        public decimal Rate { get; private set; }
        public Money ZakatDue { get; private set; }
        public DateTime CalculationDate { get; private set; }

        private ZakatCalculation() {} // For EF Core

        public ZakatCalculation(Money amount, Money nisabThreshold, decimal rate = 0.025m)
        {
            Amount = amount;
            NisabThreshold = nisabThreshold;
            Rate = rate;
            CalculationDate = DateTime.UtcNow;
            Calculate();
        }

        private void Calculate()
        {
            if (Amount.Amount >= NisabThreshold.Amount)
            {
                ZakatDue = Money.Create(Amount.Amount * Rate, Amount.Currency);
            }
            else
            {
                ZakatDue = Money.Create(0, Amount.Currency);
            }
        }
    }
}