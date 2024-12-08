namespace IslamicPOS.Infrastructure.Repositories;

public class SaleRepository : BaseRepository<Sale>, ISaleRepository
{
    public SaleRepository(ApplicationDbContext context) : base(context)
    {
    }

    public async Task<IEnumerable<Sale>> GetByDateRangeAsync(
        DateTime startDate,
        DateTime endDate)
    {
        return await _dbSet
            .Include(s => s.Items)
            .Where(s => s.Date >= startDate && s.Date <= endDate)
            .OrderByDescending(s => s.Date)
            .ToListAsync();
    }

    public async Task<decimal> GetTotalSalesAsync(DateTime date)
    {
        return await _dbSet
            .Where(s => s.Date.Date == date.Date)
            .SumAsync(s => s.Total);
    }

    public async Task<IEnumerable<SalesByProduct>> GetTopProductsAsync(
        DateTime startDate,
        DateTime endDate,
        int count = 10)
    {
        return await _dbSet
            .Where(s => s.Date >= startDate && s.Date <= endDate)
            .SelectMany(s => s.Items)
            .GroupBy(i => new { i.ProductId, i.ProductName })
            .Select(g => new SalesByProduct
            {
                ProductId = g.Key.ProductId,
                ProductName = g.Key.ProductName,
                Quantity = g.Sum(i => i.Quantity),
                Revenue = g.Sum(i => i.Total)
            })
            .OrderByDescending(x => x.Revenue)
            .Take(count)
            .ToListAsync();
    }

    public async Task<SaleSummary> GetDailySummaryAsync(DateTime date)
    {
        var sales = await _dbSet
            .Include(s => s.Items)
            .Where(s => s.Date.Date == date.Date)
            .ToListAsync();

        return new SaleSummary
        {
            Date = date,
            TotalSales = sales.Sum(s => s.Total),
            TransactionCount = sales.Count,
            ItemsSold = sales.Sum(s => s.Items.Sum(i => i.Quantity)),
            AverageTransactionValue = sales.Any() 
                ? sales.Average(s => s.Total) 
                : 0
        };
    }
}