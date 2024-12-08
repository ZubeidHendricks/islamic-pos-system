using IslamicPOS.Core.Models.Common;

namespace IslamicPOS.Core.Models.IslamicFinance;

public class MudarabahResult : Entity
{
    public decimal InvestmentAmount { get; set; }
    public decimal ProfitShare { get; set; }
    public decimal InvestorShare { get; set; }
    public decimal MudaribShare { get; set; }
    public decimal TotalReturn { get; set; }
    public decimal ROI { get; set; }
    public DateTime CalculationDate { get; set; }
    public string? Notes { get; set; }
}

public class MusharakaResult : Entity
{
    public decimal TotalInvestment { get; set; }
    public decimal Partner1Investment { get; set; }
    public decimal Partner2Investment { get; set; }
    public decimal ProfitSharingRatio1 { get; set; }
    public decimal ProfitSharingRatio2 { get; set; }
    public decimal Partner1Share { get; set; }
    public decimal Partner2Share { get; set; }
    public DateTime CalculationDate { get; set; }
}

public class Partner : Entity
{
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string PhoneNumber { get; set; } = string.Empty;
    public string Status { get; set; } = string.Empty;
    public decimal SharePercentage { get; set; }
    public DateTime JoinDate { get; set; }
    public List<PartnerShare> Shares { get; set; } = new();
}

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