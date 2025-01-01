namespace IslamicPOS.Infrastructure.Repositories;

public interface ITransactionRepository
{
    Task<Transaction?> GetByIdAsync(Guid id);
    Task<IEnumerable<Transaction>> GetAllAsync();
    Task<Transaction> AddAsync(Transaction transaction);
    Task UpdateAsync(Transaction transaction);
    Task DeleteAsync(Guid id);
    Task<IEnumerable<Transaction>> GetTransactionsByDateRangeAsync(DateTime startDate, DateTime endDate);
    Task<IEnumerable<Transaction>> GetTransactionsByCustomerAsync(string customerId);
    Task<IEnumerable<Transaction>> GetTransactionsByPaymentMethodAsync(string paymentMethodId);
    Task<decimal> GetTotalSalesAsync(DateTime startDate, DateTime endDate);
    Task<IEnumerable<TransactionItem>> GetTransactionItemsAsync(Guid transactionId);
    Task<Dictionary<string, decimal>> GetSalesByPaymentMethodAsync(DateTime startDate, DateTime endDate);
}