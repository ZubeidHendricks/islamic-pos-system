namespace IslamicPOS.Core.Services.IslamicFinance;

public class MusharakaCalculator
{
    public MusharakaResult CalculateProfit(
        Dictionary<string, decimal> capitalContributions,
        Dictionary<string, decimal> profitShares,
        decimal totalProfit)
    {
        // Validate total profit shares equal 100%
        if (profitShares.Values.Sum() != 100)
            throw new ArgumentException("Profit shares must total 100%");

        var totalCapital = capitalContributions.Values.Sum();
        var result = new MusharakaResult
        {
            TotalCapital = totalCapital,
            TotalProfit = totalProfit,
            Partners = new List<PartnerShare>()
        };

        foreach (var partner in capitalContributions.Keys)
        {
            var capitalShare = (capitalContributions[partner] / totalCapital) * 100;
            var profitShare = profitShares[partner];
            var profitAmount = Math.Round(totalProfit * (profitShare / 100), 2);

            result.Partners.Add(new PartnerShare
            {
                PartnerName = partner,
                CapitalContribution = capitalContributions[partner],
                CapitalSharePercentage = Math.Round(capitalShare, 2),
                ProfitSharePercentage = profitShare,
                ProfitAmount = profitAmount
            });
        }

        return result;
    }

    public MusharakaResult CalculateLoss(
        Dictionary<string, decimal> capitalContributions,
        decimal totalLoss)
    {
        var totalCapital = capitalContributions.Values.Sum();
        var result = new MusharakaResult
        {
            TotalCapital = totalCapital,
            TotalProfit = -totalLoss, // Negative for loss
            Partners = new List<PartnerShare>()
        };

        // In case of loss, it's shared according to capital contribution
        foreach (var partner in capitalContributions.Keys)
        {
            var capitalShare = (capitalContributions[partner] / totalCapital) * 100;
            var lossAmount = Math.Round(totalLoss * (capitalShare / 100), 2);

            result.Partners.Add(new PartnerShare
            {
                PartnerName = partner,
                CapitalContribution = capitalContributions[partner],
                CapitalSharePercentage = Math.Round(capitalShare, 2),
                ProfitSharePercentage = capitalShare, // Loss is shared based on capital
                ProfitAmount = -lossAmount // Negative for loss
            });
        }

        return result;
    }
}