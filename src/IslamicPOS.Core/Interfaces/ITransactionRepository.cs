using IslamicPOS.Core.Models;

namespace IslamicPOS.Core.Interfaces;

public interface ITransactionRepository
{
    Task<Transaction> CreateAsync(Transaction transaction);
    Task<Transaction?> GetByIdAsync(Guid id);
    Task<IEnumerable<Transaction>> GetByDateRangeAsync(DateTime startDate, DateTime endDate);
    Task<decimal> GetTotalRevenueAsync(DateTime startDate, DateTime endDate);
}