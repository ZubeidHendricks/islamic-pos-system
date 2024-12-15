using IslamicPOS.Domain.Common;

namespace IslamicPOS.Domain.Finance.Models;

public class PaymentMethod : EntityBase
{
    public string Name { get; private set; } = string.Empty;
    public string Description { get; private set; } = string.Empty;
    public bool IsShariahCompliant { get; private set; }
    public string ComplianceNotes { get; private set; } = string.Empty;
    public bool IsActive { get; private set; }

    public PaymentMethod(string name, string description, bool isShariahCompliant, string complianceNotes)
    {
        Name = name;
        Description = description;
        IsShariahCompliant = isShariahCompliant;
        ComplianceNotes = complianceNotes;
        IsActive = true;
    }

    // Required by EF Core
    protected PaymentMethod() { }
}
