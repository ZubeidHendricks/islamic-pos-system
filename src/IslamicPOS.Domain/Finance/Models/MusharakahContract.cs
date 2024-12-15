using IslamicPOS.Domain.Common;

namespace IslamicPOS.Domain.Finance.Models;

public class MusharakahContract : EntityBase
{
    public Money TotalCapital { get; private set; } = Money.Zero();
    public decimal BusinessSharePercentage { get; private set; }
    public decimal InvestorSharePercentage { get; private set; }
    public DateTime StartDate { get; private set; }
    public DateTime? EndDate { get; private set; }
    public string BusinessId { get; private set; } = string.Empty;
    public string InvestorId { get; private set; } = string.Empty;
    public string Terms { get; private set; } = string.Empty;
    public bool IsActive { get; private set; }

    public MusharakahContract(
        Money totalCapital,
        decimal businessShare,
        decimal investorShare,
        DateTime startDate,
        string businessId,
        string investorId,
        string terms)
    {
        TotalCapital = totalCapital;
        BusinessSharePercentage = businessShare;
        InvestorSharePercentage = investorShare;
        StartDate = startDate;
        BusinessId = businessId;
        InvestorId = investorId;
        Terms = terms;
        IsActive = true;
    }

    // Required by EF Core
    protected MusharakahContract() { }
}
