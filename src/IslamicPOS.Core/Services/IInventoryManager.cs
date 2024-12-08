using IslamicPOS.Core.Models;

namespace IslamicPOS.Core.Services;

public interface IInventoryManager
{
    Task<bool> ProcessSale(Transaction transaction);
    Task<bool> ProcessReturn(Transaction transaction);
    Task<bool> AdjustStock(Guid productId, int adjustment, string reason);
    Task<StockAdjustment> GetStockAdjustmentHistory(Guid productId);
    Task<List<StockAlert>> GetLowStockAlerts(int threshold = 10);
    Task<bool> ValidateStock(List<TransactionItem> items);
    Task<decimal> GetInventoryValue();
}