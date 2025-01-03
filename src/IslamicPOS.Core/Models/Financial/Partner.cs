using IslamicPOS.Core.Common;

namespace IslamicPOS.Core.Models.Financial;

public class Partner : Entity
{
    public string Name { get; set; } = string.Empty;
    public string ContactNumber { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public decimal SharePercentage { get; set; }
    public bool IsActive { get; set; } = true;
    public string Notes { get; set; } = string.Empty;
}