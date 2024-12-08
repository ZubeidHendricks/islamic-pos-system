namespace IslamicPOS.Core.Models.Financial
{
    public class DistributionResult
    {
        public bool Success { get; set; }
        public ProfitDistribution Distribution { get; set; }
        public string Message { get; set; }
        public List<string> Errors { get; set; }
    }
}