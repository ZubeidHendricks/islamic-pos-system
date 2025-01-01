using IslamicPOS.Core.Common;

namespace IslamicPOS.Core.Models.IslamicFinance;

public class PaymentMethod : Entity
{
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public bool IsShariahCompliant { get; set; }
    public string ComplianceNotes { get; set; } = string.Empty;
    public bool RequiresApproval { get; set; }
    public decimal? MaximumAmount { get; set; }
    public decimal? MinimumAmount { get; set; }
}