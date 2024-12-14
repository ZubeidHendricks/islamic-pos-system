using IslamicPOS.Domain.Common;

namespace IslamicPOS.Domain.Finance
{
    public class ZakaatCalculation
    {
        public decimal Amount { get; set; }
        public decimal ZakaatDue { get; set; }
        public DateTime CalculationDate { get; set; }
    }

    public class MudarabahContract
    {
        public decimal InvestmentAmount { get; set; }
        public decimal ProfitSharingRatio { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }

    public class MusharakahContract
    {
        public decimal TotalInvestment { get; set; }
        public decimal BankShare { get; set; }
        public decimal CustomerShare { get; set; }
        public DateTime ContractDate { get; set; }
    }
}