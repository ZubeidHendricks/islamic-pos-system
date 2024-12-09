using Microsoft.Extensions.Configuration;
using IslamicPOS.Core.Models.IslamicFinance;

namespace IslamicPOS.Core.Services.ZakaatCalculator
{
    public class ZakaatService : IZakaatService
    {
        private readonly decimal _nisabThreshold;
        private readonly decimal _zakaatRate;

        public ZakaatService(IConfiguration configuration)
        {
            _nisabThreshold = configuration.GetValue<decimal>("ZakaatSettings:NisabThreshold");
            _zakaatRate = configuration.GetValue<decimal>("ZakaatSettings:ZakaatRate");
        }

        public ZakaatCalculation CalculateZakaat(ZakaatInput input)
        {
            var totalWealth = CalculateTotalWealth(input);
            
            return new ZakaatCalculation
            {
                TotalWealth = totalWealth,
                NisabThreshold = _nisabThreshold,
                IsZakaatDue = totalWealth >= _nisabThreshold,
                ZakaatAmount = CalculateZakaatAmount(totalWealth),
                CalculationDate = DateTime.UtcNow
            };
        }

        private decimal CalculateTotalWealth(ZakaatInput input)
        {
            return input.CashOnHand +
                   input.BankBalance +
                   input.GoldValue +
                   input.SilverValue +
                   input.StockValue +
                   input.BusinessAssets +
                   input.Investments -
                   input.Debts;
        }

        private decimal CalculateZakaatAmount(decimal totalWealth)
        {
            return totalWealth >= _nisabThreshold
                ? totalWealth * _zakaatRate
                : 0;
        }
    }
}