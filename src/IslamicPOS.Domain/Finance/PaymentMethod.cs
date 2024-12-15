using IslamicPOS.Domain.Common;

namespace IslamicPOS.Domain.Finance;

public class PaymentMethod : Entity
{
    public string Name { get; set; } = string.Empty;
    public string Type { get; set; } = string.Empty;
    public bool IsShariahCompliant { get; set; }
    public string ComplianceNotes { get; set; } = string.Empty;
    public bool IsActive { get; set; }

    public bool ValidateCompliance()
    {
        // Implement Shariah compliance validation logic
        return IsShariahCompliant;
    }
}