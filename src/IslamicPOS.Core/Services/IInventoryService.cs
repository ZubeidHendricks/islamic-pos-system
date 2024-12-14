using IslamicPOS.Core.Models.Product;
using IslamicPOS.Core.Models.Transaction;

namespace IslamicPOS.Core.Services
{
    public interface IInventoryService
    {
        Task<bool> ValidateStock(Transaction transaction);
        Task UpdateStock(Transaction transaction);
        Task<int> GetStockLevel(int productId);
        Task<IEnumerable<Product>> GetLowStockProducts(int threshold = 10);
    }
}