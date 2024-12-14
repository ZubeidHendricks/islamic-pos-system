namespace IslamicPOS.Core.Models.IslamicFinance.Shared
{
    public class PartnerShareModel
    {
        public string PartnerName { get; set; } = string.Empty;
        public decimal CapitalContribution { get; set; }
        public decimal CapitalSharePercentage { get; set; }
        public decimal ProfitSharePercentage { get; set; }
        public decimal ProfitAmount { get; set; }
    }
}