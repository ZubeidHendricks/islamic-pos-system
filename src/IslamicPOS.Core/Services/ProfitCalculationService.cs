using IslamicPOS.Core.Interfaces;

namespace IslamicPOS.Core.Services;

public class ProfitCalculationService
{
    private readonly ITransactionRepository _transactionRepository;

    public ProfitCalculationService(ITransactionRepository transactionRepository)
    {
        _transactionRepository = transactionRepository;
    }

    public async Task<decimal> CalculateProfitAsync(DateTime startDate, DateTime endDate)
    {
        var revenue = await _transactionRepository.GetTotalRevenueAsync(startDate, endDate);
        // TODO: Include cost calculations for accurate profit
        return revenue;
    }

    public IDictionary<string, decimal> CalculateShares(decimal totalProfit, IDictionary<string, decimal> shareholderPercentages)
    {
        var shares = new Dictionary<string, decimal>();
        var totalPercentage = shareholderPercentages.Values.Sum();

        if (totalPercentage != 100)
        {
            throw new ArgumentException("Total percentage must equal 100");
        }

        foreach (var (shareholder, percentage) in shareholderPercentages)
        {
            shares[shareholder] = Math.Round(totalProfit * (percentage / 100), 2);
        }

        return shares;
    }
}