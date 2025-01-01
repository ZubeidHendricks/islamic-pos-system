using IslamicPOS.Core.Common;
using IslamicPOS.Core.Models.Financial;

namespace IslamicPOS.Core.Models.IslamicFinance;

public class ProfitDistributionDetail : Entity
{
    public Guid ProfitSharingId { get; set; }
    public ProfitSharing? ProfitSharing { get; set; }
    
    public Guid PartnerId { get; set; }
    public Partner? Partner { get; set; }
    
    public decimal Amount { get; set; }
    public decimal Percentage { get; set; }
    public bool IsPaid { get; set; }
    public DateTime? PaymentDate { get; set; }
    public string PaymentReference { get; set; } = string.Empty;
    public string Notes { get; set; } = string.Empty;
}