using IslamicPOS.Core.Common;

namespace IslamicPOS.Core.Models.Financial;

public class Partner : Entity
{
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Phone { get; set; } = string.Empty;
    public decimal InvestmentAmount { get; set; }
    public decimal ProfitSharePercentage { get; set; }
    public DateTime JoinDate { get; set; }
    public bool IsActive { get; set; }
    public string Notes { get; set; } = string.Empty;
}