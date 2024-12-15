using IslamicPOS.Domain.Common;

namespace IslamicPOS.Domain.Finance;

public class Partner : Entity
{
    public string Name { get; set; } = string.Empty;
    public string Type { get; set; } = string.Empty; // Investor, Supplier, etc.
    public decimal ProfitSharePercentage { get; set; }
    public string BankAccount { get; set; } = string.Empty;
    public string ContactDetails { get; set; } = string.Empty;
    public bool IsActive { get; set; }
}