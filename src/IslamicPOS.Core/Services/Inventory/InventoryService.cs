using IslamicPOS.Core.Models.Inventory;

namespace IslamicPOS.Core.Services.Inventory
{
    public interface IInventoryService
    {
        Task<StockMovement> AddStockMovement(StockMovement movement);
        Task<StockMovement> GetStockMovement(int id);
        Task<List<StockMovement>> GetStockMovements(DateTime startDate, DateTime endDate);
        Task<List<StockMovement>> GetProductMovements(int productId);
        Task<int> GetCurrentStock(int productId);
        Task UpdateStockLevel(int productId, int quantity);
    }
}