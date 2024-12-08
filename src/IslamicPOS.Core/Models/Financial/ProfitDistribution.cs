namespace IslamicPOS.Core.Models.Financial
{
    public class ProfitDistribution
    {
        public int Id { get; set; }
        public DateTime DistributionDate { get; set; }
        public decimal TotalProfit { get; set; }
        public decimal ZakaahAmount { get; set; }
        public decimal DistributableProfit { get; set; }
        public List<PartnerShare> PartnerShares { get; set; }
        public string Status { get; set; }
        public string Notes { get; set; }
    }
}