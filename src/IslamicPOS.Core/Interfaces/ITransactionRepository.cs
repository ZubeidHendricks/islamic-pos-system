using IslamicPOS.Core.Models.Transaction;

namespace IslamicPOS.Core.Interfaces
{
    public interface ITransactionRepository
    {
        Task<Transaction> GetByIdAsync(int id);
        Task<IEnumerable<Transaction>> GetAllAsync();
        Task<Transaction> AddAsync(Transaction transaction);
        Task UpdateAsync(Transaction transaction);
    }
}