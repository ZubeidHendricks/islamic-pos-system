namespace IslamicPOS.Core.Services.IslamicFinance;

public class MudarabahCalculator
{
    public MudarabahResult CalculateProfit(
        decimal capital,
        decimal totalProfit,
        decimal rabAlMalShare,
        decimal mudaribShare)
    {
        if (rabAlMalShare + mudaribShare != 100)
            throw new ArgumentException("Profit sharing ratios must total 100%");

        var result = new MudarabahResult
        {
            Capital = capital,
            TotalProfit = totalProfit,
            RabAlMalShare = rabAlMalShare,
            MudaribShare = mudaribShare,
            RabAlMalAmount = Math.Round(totalProfit * (rabAlMalShare / 100), 2),
            MudaribAmount = Math.Round(totalProfit * (mudaribShare / 100), 2)
        };

        // In case of loss, it's borne by the capital provider (Rab al-Mal)
        if (totalProfit < 0)
        {
            result.RabAlMalAmount = totalProfit;
            result.MudaribAmount = 0;
        }

        return result;
    }
}