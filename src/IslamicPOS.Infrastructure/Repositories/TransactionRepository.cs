using IslamicPOS.Infrastructure.Persistence;

namespace IslamicPOS.Infrastructure.Repositories;

public class TransactionRepository : BaseRepository<Transaction>, ITransactionRepository
{
    public TransactionRepository(ApplicationDbContext context) : base(context)
    {
    }

    public override async Task<Transaction?> GetByIdAsync(Guid id)
    {
        return await _dbSet
            .Include(t => t.Items)
                .ThenInclude(i => i.Product)
            .Include(t => t.PaymentMethod)
            .FirstOrDefaultAsync(t => t.Id == id);
    }

    public async Task<IEnumerable<Transaction>> GetTransactionsByDateRangeAsync(DateTime startDate, DateTime endDate)
    {
        return await _dbSet
            .Include(t => t.Items)
            .Include(t => t.PaymentMethod)
            .Where(t => t.TransactionDate >= startDate && t.TransactionDate <= endDate)
            .OrderByDescending(t => t.TransactionDate)
            .ToListAsync();
    }

    public async Task<IEnumerable<Transaction>> GetTransactionsByCustomerAsync(string customerId)
    {
        return await _dbSet
            .Include(t => t.Items)
            .Where(t => t.CustomerId == customerId)
            .OrderByDescending(t => t.TransactionDate)
            .ToListAsync();
    }

    public async Task<IEnumerable<Transaction>> GetTransactionsByPaymentMethodAsync(string paymentMethodId)
    {
        return await _dbSet
            .Include(t => t.Items)
            .Where(t => t.PaymentMethodId == paymentMethodId)
            .OrderByDescending(t => t.TransactionDate)
            .ToListAsync();
    }

    public async Task<decimal> GetTotalSalesAsync(DateTime startDate, DateTime endDate)
    {
        return await _dbSet
            .Where(t => t.TransactionDate >= startDate && t.TransactionDate <= endDate)
            .SumAsync(t => t.TotalAmount);
    }

    public async Task<IEnumerable<TransactionItem>> GetTransactionItemsAsync(Guid transactionId)
    {
        return await _context.TransactionItems
            .Include(ti => ti.Product)
            .Where(ti => ti.TransactionId == transactionId)
            .OrderBy(ti => ti.Product!.Name)
            .ToListAsync();
    }

    public async Task<Dictionary<string, decimal>> GetSalesByPaymentMethodAsync(DateTime startDate, DateTime endDate)
    {
        var salesByMethod = await _dbSet
            .Include(t => t.PaymentMethod)
            .Where(t => t.TransactionDate >= startDate && t.TransactionDate <= endDate)
            .GroupBy(t => t.PaymentMethod!.Name)
            .Select(g => new
            {
                PaymentMethod = g.Key,
                TotalSales = g.Sum(t => t.TotalAmount)
            })
            .ToListAsync();

        return salesByMethod.ToDictionary(x => x.PaymentMethod, x => x.TotalSales);
    }
}