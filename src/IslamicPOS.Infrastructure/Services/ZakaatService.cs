using IslamicPOS.Domain.Finance;
using IslamicPOS.Domain.ValueObjects;
using Microsoft.Extensions.Configuration;

namespace IslamicPOS.Infrastructure.Services
{
    public class ZakaatService : IZakaatService
    {
        private readonly decimal _nisabThreshold;
        private readonly decimal _zakaatRate;
        private readonly string _currency;

        public ZakaatService(IConfiguration configuration)
        {
            _nisabThreshold = configuration.GetValue<decimal>("ZakaatSettings:NisabThreshold");
            _zakaatRate = configuration.GetValue<decimal>("ZakaatSettings:ZakaatRate", 0.025m); // Default to 2.5%
            _currency = configuration.GetValue<string>("ZakaatSettings:Currency", "USD");
        }

        public ZakaatCalculation CalculateZakaat(ZakaatInput input)
        {
            var totalWealth = CalculateTotalWealth(input);
            var nisabThreshold = new Money(_nisabThreshold, _currency);
            var isZakaatDue = totalWealth.Amount >= _nisabThreshold;
            var zakaatAmount = isZakaatDue ? totalWealth.Multiply(_zakaatRate) : Money.Zero(_currency);

            return new ZakaatCalculation
            {
                TotalWealth = totalWealth,
                NisabThreshold = nisabThreshold,
                ZakaatAmount = zakaatAmount,
                IsZakaatDue = isZakaatDue,
                CalculationDate = DateTime.UtcNow
            };
        }

        private Money CalculateTotalWealth(ZakaatInput input)
        {
            var total = input.CashOnHand +
                        input.BankBalance +
                        input.GoldValue +
                        input.SilverValue +
                        input.StockValue +
                        input.BusinessAssets +
                        input.Investments -
                        input.Debts;

            return new Money(total, _currency);
        }
    }
}