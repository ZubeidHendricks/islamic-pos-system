using IslamicPOS.Core.Common;

namespace IslamicPOS.Core.Finance;

public class MusharakahContract : Entity
{
    public Money TotalInvestment { get; private set; }
    public Money Partner1Investment { get; private set; }
    public Money Partner2Investment { get; private set; }
    public decimal Partner1Share { get; private set; } // As a percentage
    public decimal Partner2Share { get; private set; } // As a percentage
    public string ProjectDetails { get; private set; }
    public DateTime StartDate { get; private set; }
    
    private MusharakahContract(
        Money partner1Investment,
        Money partner2Investment,
        decimal partner1Share,
        decimal partner2Share,
        string projectDetails,
        DateTime startDate)
    {
        Partner1Investment = partner1Investment;
        Partner2Investment = partner2Investment;
        Partner1Share = partner1Share;
        Partner2Share = partner2Share;
        ProjectDetails = projectDetails;
        StartDate = startDate;

        TotalInvestment = partner1Investment.Add(partner2Investment);
    }

    public static MusharakahContract Create(
        Money partner1Investment,
        Money partner2Investment,
        decimal partner1Share,
        decimal partner2Share,
        string projectDetails,
        DateTime startDate)
    {
        if (partner1Share + partner2Share != 1)
            throw new ArgumentException("Partner shares must sum to 100%");

        if (partner1Investment.Currency != partner2Investment.Currency)
            throw new ArgumentException("All investments must be in the same currency");

        return new MusharakahContract(
            partner1Investment,
            partner2Investment,
            partner1Share,
            partner2Share,
            projectDetails,
            startDate);
    }
}