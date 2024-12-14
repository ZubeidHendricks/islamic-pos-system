using IslamicPOS.Core.Models.Common;

namespace IslamicPOS.Core.Models.IslamicFinance
{
    public class ProfitSharing
    {
        public Money TotalProfit { get; private set; }
        public decimal MerchantShare { get; private set; }
        public decimal PartnerShare { get; private set; }
        public Money MerchantAmount { get; private set; }
        public Money PartnerAmount { get; private set; }
        public DateTime CalculationDate { get; private set; }

        private ProfitSharing() {} // For EF Core

        public ProfitSharing(Money totalProfit, decimal merchantShare)
        {
            TotalProfit = totalProfit;
            MerchantShare = merchantShare;
            PartnerShare = 1 - merchantShare;
            CalculationDate = DateTime.UtcNow;
            Calculate();
        }

        private void Calculate()
        {
            MerchantAmount = Money.Create(TotalProfit.Amount * MerchantShare, TotalProfit.Currency);
            PartnerAmount = Money.Create(TotalProfit.Amount * PartnerShare, TotalProfit.Currency);
        }
    }
}