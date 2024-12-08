using IslamicPOS.Core.Models.InventoryManagement;

namespace IslamicPOS.Core.Services;

public interface IPurchaseOrderService
{
    Task<PurchaseOrder> CreatePurchaseOrder(PurchaseOrder order);
    Task<PurchaseOrder> UpdatePurchaseOrder(PurchaseOrder order);
    Task<bool> DeletePurchaseOrder(int id);
    Task<PurchaseOrder> GetPurchaseOrderById(int id);
    Task<List<PurchaseOrder>> GetPurchaseOrders(DateTime? startDate = null, DateTime? endDate = null);
    Task<List<PurchaseOrder>> GetPurchaseOrdersByStatus(string status);
    Task<bool> UpdateOrderStatus(int orderId, string status);
    Task<decimal> GetTotalOrdersValue(DateTime startDate, DateTime endDate);
    Task<List<PurchaseOrder>> GetSupplierOrders(int supplierId);
}