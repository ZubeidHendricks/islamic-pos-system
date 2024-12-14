using IslamicPOS.Core.Models.Transaction;

namespace IslamicPOS.Core.Services
{
    public interface IInventoryManager
    {
        Task<bool> ValidateStock(Transaction transaction);
        Task ProcessTransaction(Transaction transaction);
        Task<int> GetStockLevel(string productId);
        Task UpdateStock(TransactionItem item, int quantity);
    }
}