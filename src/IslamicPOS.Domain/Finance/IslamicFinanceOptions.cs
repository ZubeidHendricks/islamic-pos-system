using IslamicPOS.Domain.Common;
using IslamicPOS.Domain.ValueObjects;

namespace IslamicPOS.Domain.Finance
{
    public class IslamicFinanceOptions : BaseEntity
    {
        public Money NisabThreshold { get; private set; }
        public decimal ZakaatRate { get; private set; }
        public decimal DefaultProfitSharingRatio { get; private set; }
        public int FinancingTermInMonths { get; private set; }

        private IslamicFinanceOptions() { } // For EF Core

        public IslamicFinanceOptions(Money nisabThreshold, decimal zakaatRate, 
            decimal defaultProfitSharingRatio, int financingTermInMonths)
        {
            if (zakaatRate <= 0 || zakaatRate > 1)
                throw new ArgumentException("Zakaat rate must be between 0 and 1", nameof(zakaatRate));

            if (defaultProfitSharingRatio <= 0 || defaultProfitSharingRatio > 1)
                throw new ArgumentException("Profit sharing ratio must be between 0 and 1", 
                    nameof(defaultProfitSharingRatio));

            if (financingTermInMonths <= 0)
                throw new ArgumentException("Financing term must be positive", 
                    nameof(financingTermInMonths));

            NisabThreshold = nisabThreshold;
            ZakaatRate = zakaatRate;
            DefaultProfitSharingRatio = defaultProfitSharingRatio;
            FinancingTermInMonths = financingTermInMonths;
        }

        public static IslamicFinanceOptions CreateDefault(string currency)
        {
            return new IslamicFinanceOptions(
                nisabThreshold: Money.FromDecimal(5000, currency), // Example threshold
                zakaatRate: 0.025m, // 2.5%
                defaultProfitSharingRatio: 0.7m, // 70:30 split
                financingTermInMonths: 12
            );
        }
    }
}