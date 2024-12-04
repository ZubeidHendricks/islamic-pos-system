using IslamicPOS.Core.Models.InventoryManagement;

namespace IslamicPOS.Core.Services.InventoryManagement;

public interface IInventoryService
{
    Task<StockMovement> AddStock(Guid productId, int quantity, string reference, string notes);
    Task<StockMovement> RemoveStock(Guid productId, int quantity, string reference, string notes);
    Task<StockMovement> AdjustStock(Guid productId, int newQuantity, string notes);
    Task<IEnumerable<StockMovement>> GetStockMovements(Guid productId, DateTime? startDate = null, DateTime? endDate = null);
    Task<int> GetCurrentStock(Guid productId);
    Task<IEnumerable<Product>> GetLowStockProducts(int threshold = 10);
    Task<PurchaseOrder> CreatePurchaseOrder(PurchaseOrder order);
    Task<bool> UpdatePurchaseOrderStatus(int orderId, string status);
    Task<bool> ReceivePurchaseOrder(int orderId, IEnumerable<(Guid ProductId, int ReceivedQuantity)> receivedItems);
}