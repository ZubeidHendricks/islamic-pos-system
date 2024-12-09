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

    public interface IZakaatService
    {
        ZakaatCalculation CalculateZakaat(ZakaatInput input);
    }
}