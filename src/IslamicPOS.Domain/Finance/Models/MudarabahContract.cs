using IslamicPOS.Domain.Common;

namespace IslamicPOS.Domain.Finance.Models;

public class MudarabahContract : EntityBase
{
    public Money InvestmentAmount { get; private set; } = Money.Zero();
    public decimal ProfitSharingRatio { get; private set; } // Percentage for investor
    public DateTime StartDate { get; private set; }
    public DateTime? EndDate { get; private set; }
    public string InvestorId { get; private set; } = string.Empty;
    public string BusinessId { get; private set; } = string.Empty;
    public string Terms { get; private set; } = string.Empty;
    public bool IsActive { get; private set; }

    public MudarabahContract(
        Money investmentAmount,
        decimal profitSharingRatio,
        DateTime startDate,
        string investorId,
        string businessId,
        string terms)
    {
        InvestmentAmount = investmentAmount;
        ProfitSharingRatio = profitSharingRatio;
        StartDate = startDate;
        InvestorId = investorId;
        BusinessId = businessId;
        Terms = terms;
        IsActive = true;
    }

    // Required by EF Core
    protected MudarabahContract() { }
}
