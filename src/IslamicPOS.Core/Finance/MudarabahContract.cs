using IslamicPOS.Core.Common;

namespace IslamicPOS.Core.Finance;

public class MudarabahContract : Entity
{
    public Money InvestmentAmount { get; private set; }
    public decimal ProfitSharingRatio { get; private set; } // As a percentage
    public string InvestorDetails { get; private set; }
    public string ProjectDetails { get; private set; }
    public DateTime StartDate { get; private set; }
    public DateTime? EndDate { get; private set; }
    
    private MudarabahContract(
        Money investmentAmount,
        decimal profitSharingRatio,
        string investorDetails,
        string projectDetails,
        DateTime startDate,
        DateTime? endDate = null)
    {
        InvestmentAmount = investmentAmount;
        ProfitSharingRatio = profitSharingRatio;
        InvestorDetails = investorDetails;
        ProjectDetails = projectDetails;
        StartDate = startDate;
        EndDate = endDate;
    }

    public static MudarabahContract Create(
        Money investmentAmount,
        decimal profitSharingRatio,
        string investorDetails,
        string projectDetails,
        DateTime startDate,
        DateTime? endDate = null)
    {
        if (profitSharingRatio is < 0 or > 1)
            throw new ArgumentException("Profit sharing ratio must be between 0 and 1");

        return new MudarabahContract(
            investmentAmount,
            profitSharingRatio,
            investorDetails,
            projectDetails,
            startDate,
            endDate);
    }
}