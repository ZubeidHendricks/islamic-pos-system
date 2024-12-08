using IslamicPOS.Core.Models.Common;

namespace IslamicPOS.Core.Models.Financial;

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