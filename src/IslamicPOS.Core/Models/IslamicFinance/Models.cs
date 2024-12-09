namespace IslamicPOS.Core.Models.IslamicFinance
{
    public class MudarabahResult
    {
        public decimal InvestedCapital { get; set; }
        public decimal Profit { get; set; }
        public decimal RabbulMaalShare { get; set; }
        public decimal MudaribShare { get; set; }
        public decimal ProfitSharingRatio { get; set; }
    }

    public class MusharakaResult
    {
        public decimal TotalCapital { get; set; }
        public decimal TotalProfit { get; set; }
        public List<PartnerShare> PartnerShares { get; set; } = new();
    }

    public class PartnerShare
    {
        public string PartnerId { get; set; } = string.Empty;
        public decimal InvestedAmount { get; set; }
        public decimal SharePercentage { get; set; }
        public decimal ProfitShare { get; set; }
    }

    public class ZakaatInput
    {
        public decimal CashOnHand { get; set; }
        public decimal BankBalance { get; set; }
        public decimal GoldValue { get; set; }
        public decimal SilverValue { get; set; }
        public decimal StockValue { get; set; }
        public decimal BusinessAssets { get; set; }
        public decimal Debts { get; set; }
        public decimal Investments { get; set; }
    }

    public class ZakaatCalculation
    {
        public decimal TotalWealth { get; set; }
        public decimal NisabThreshold { get; set; }
        public decimal ZakaatAmount { get; set; }
        public bool IsZakaatDue { get; set; }
        public DateTime CalculationDate { get; set; }
    }
}