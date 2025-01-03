using IslamicPOS.Core.Common;

namespace IslamicPOS.Core.Models.IslamicFinance;

public class ProfitSharing : Entity
{
    public decimal TotalAmount { get; set; }
    public DateTime PeriodStart { get; set; }
    public DateTime PeriodEnd { get; set; }
    public string Description { get; set; } = string.Empty;
    public string Period { get; set; } = string.Empty;
    public bool IsDistributed { get; set; }
    public DateTime? DistributedAt { get; set; }
    public virtual ICollection<ProfitDistributionDetail> DistributionDetails { get; set; } = new List<ProfitDistributionDetail>();
}