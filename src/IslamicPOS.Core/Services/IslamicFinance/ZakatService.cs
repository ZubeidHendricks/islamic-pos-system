using IslamicPOS.Core.Models.Common;
using IslamicPOS.Core.Models.IslamicFinance;
using Microsoft.Extensions.Configuration;

namespace IslamicPOS.Core.Services.IslamicFinance
{
    public class ZakatService
    {
        private readonly IConfiguration _configuration;
        private readonly Money _nisabThreshold;

        public ZakatService(IConfiguration configuration)
        {
            _configuration = configuration;
            var nisabAmount = _configuration.GetValue<decimal>("IslamicFinance:NisabThreshold");
            _nisabThreshold = Money.Create(nisabAmount);
        }

        public async Task<ZakatCalculation> CalculateZakat(Money amount)
        {
            var zakatRate = _configuration.GetValue<decimal>("IslamicFinance:ZakatRate", 0.025m); // Default 2.5%
            return new ZakatCalculation(amount, _nisabThreshold, zakatRate);
        }

        public async Task<bool> ValidateZakatAmount(Money amount)
        {
            return amount >= _nisabThreshold;
        }

        public async Task<Money> GetNisabThreshold()
        {
            return _nisabThreshold;
        }
    }
}