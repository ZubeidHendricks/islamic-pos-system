using IslamicPOS.Infrastructure.Repositories;

namespace IslamicPOS.Infrastructure.Services;

public class FinancialService : IFinancialService
{
    private readonly ITransactionRepository _transactionRepository;
    private readonly IProductRepository _productRepository;
    private readonly IPartnerRepository _partnerRepository;

    public FinancialService(
        ITransactionRepository transactionRepository,
        IProductRepository productRepository,
        IPartnerRepository partnerRepository)
    {
        _transactionRepository = transactionRepository;
        _productRepository = productRepository;
        _partnerRepository = partnerRepository;
    }

    public async Task<decimal> CalculateRevenue(DateTime startDate, DateTime endDate)
    {
        return await _transactionRepository.GetTotalSalesAsync(startDate, endDate);
    }

    public async Task<decimal> CalculateProfit(DateTime startDate, DateTime endDate)
    {
        var transactions = await _transactionRepository.GetTransactionsByDateRangeAsync(startDate, endDate);
        decimal totalProfit = 0;

        foreach (var transaction in transactions)
        {
            foreach (var item in transaction.Items)
            {
                var profit = (item.UnitPrice - item.Product!.Cost) * item.Quantity;
                totalProfit += profit;
            }
        }

        return totalProfit;
    }

    public async Task<decimal> CalculateOperatingCosts(DateTime startDate, DateTime endDate)
    {
        // This would typically include fixed costs, variable costs, and overhead
        // For now, returning a placeholder calculation
        var revenue = await CalculateRevenue(startDate, endDate);
        return revenue * 0.3m; // Assuming 30% operating costs
    }

    public async Task<IDictionary<string, decimal>> GetProfitByCategory(DateTime startDate, DateTime endDate)
    {
        var transactions = await _transactionRepository.GetTransactionsByDateRangeAsync(startDate, endDate);
        var profitByCategory = new Dictionary<string, decimal>();

        foreach (var transaction in transactions)
        {
            foreach (var item in transaction.Items)
            {
                var profit = (item.UnitPrice - item.Product!.Cost) * item.Quantity;
                var category = item.Product.Category;

                if (profitByCategory.ContainsKey(category))
                    profitByCategory[category] += profit;
                else
                    profitByCategory[category] = profit;
            }
        }

        return profitByCategory;
    }

    public async Task<IEnumerable<Product>> GetMostProfitableProducts(int count = 10)
    {
        var products = await _productRepository.GetAllAsync();
        return products
            .OrderByDescending(p => p.Price - p.Cost)
            .Take(count)
            .ToList();
    }

    public async Task<decimal> GetInvestorReturns(Guid partnerId, DateTime startDate, DateTime endDate)
    {
        var partner = await _partnerRepository.GetByIdAsync(partnerId);
        if (partner == null) return 0;

        var totalProfit = await CalculateProfit(startDate, endDate);
        return totalProfit * (partner.ProfitSharePercentage / 100m);
    }

    public async Task<decimal> CalculateCharityObligations(DateTime startDate, DateTime endDate)
    {
        var profit = await CalculateProfit(startDate, endDate);
        return profit * 0.025m; // 2.5% charity obligation
    }
}