using IslamicPOS.Core.Models.Common;

namespace IslamicPOS.Core.Models.Financial;

public class ProfitDistribution : Entity
{
    public DateTime DistributionDate { get; set; }
    public decimal TotalProfit { get; set; }
    public decimal ZakaatAmount { get; set; }
    public decimal DistributableProfit { get; set; }
    public List<PartnerShare> PartnerShares { get; set; } = new();
    public string Status { get; set; } = string.Empty;
    public string? Notes { get; set; }
}

public class PartnerShare : Entity
{
    public Guid ProfitDistributionId { get; set; }
    public ProfitDistribution ProfitDistribution { get; set; } = null!;
    public Guid PartnerId { get; set; }
    public Partner Partner { get; set; } = null!;
    public decimal SharePercentage { get; set; }
    public decimal Amount { get; set; }
    public string Status { get; set; } = string.Empty;
    public DateTime? PaidDate { get; set; }
}

public class DistributionResult
{
    public bool Success { get; set; }
    public ProfitDistribution Distribution { get; set; } = null!;
    public string Message { get; set; } = string.Empty;
    public List<string> Errors { get; set; } = new();
}

public class DistributionHistory : Entity
{
    public Guid ProfitDistributionId { get; set; }
    public ProfitDistribution ProfitDistribution { get; set; } = null!;
    public string Action { get; set; } = string.Empty;
    public string Details { get; set; } = string.Empty;
    public string Status { get; set; } = string.Empty;
}