namespace IslamicPOS.Infrastructure.Services;

public interface IFinancialService
{
    Task<decimal> CalculateRevenue(DateTime startDate, DateTime endDate);
    Task<decimal> CalculateProfit(DateTime startDate, DateTime endDate);
    Task<decimal> CalculateOperatingCosts(DateTime startDate, DateTime endDate);
    Task<IDictionary<string, decimal>> GetProfitByCategory(DateTime startDate, DateTime endDate);
    Task<IEnumerable<Product>> GetMostProfitableProducts(int count = 10);
    Task<decimal> GetInvestorReturns(Guid partnerId, DateTime startDate, DateTime endDate);
    Task<decimal> CalculateCharityObligations(DateTime startDate, DateTime endDate);
}