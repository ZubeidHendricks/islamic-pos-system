namespace IslamicPOS.Core.Models.Financial
{
    public class PartnerShare
    {
        public int Id { get; set; }
        public int ProfitDistributionId { get; set; }
        public ProfitDistribution ProfitDistribution { get; set; }
        public int PartnerId { get; set; }
        public Partner Partner { get; set; }
        public decimal SharePercentage { get; set; }
        public decimal Amount { get; set; }
        public string Status { get; set; }
        public DateTime? PaidDate { get; set; }
    }
}