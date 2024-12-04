namespace IslamicPOS.Core.Services;

public class ZakaahCalculationService
{
    public decimal CalculateZakaah(decimal assets, decimal liabilities)
    {
        var netAssets = assets - liabilities;
        var nisabThreshold = GetNisabThreshold();

        if (netAssets < nisabThreshold)
        {
            return 0;
        }

        return Math.Round(netAssets * 0.025m, 2); // 2.5% of net assets
    }

    private decimal GetNisabThreshold()
    {
        // TODO: Implement dynamic Nisab calculation based on gold/silver prices
        // For now, using a fixed value (approximate value of 85g of gold)
        return 5000m;
    }
}