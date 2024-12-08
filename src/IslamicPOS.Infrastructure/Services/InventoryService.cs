using IslamicPOS.Core.Models.InventoryManagement;
using IslamicPOS.Core.Services.InventoryManagement;
using IslamicPOS.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace IslamicPOS.Infrastructure.Services;

public class InventoryService : IInventoryService
{
    private readonly ApplicationDbContext _context;
    private readonly ILogger<InventoryService> _logger;

    public InventoryService(ApplicationDbContext context, ILogger<InventoryService> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<StockMovement> AddStock(Guid productId, int quantity, string reference, string notes)
    {
        if (quantity <= 0)
            throw new ArgumentException("Quantity must be positive", nameof(quantity));

        var product = await _context.Products.FindAsync(productId)
            ?? throw new KeyNotFoundException($"Product with ID {productId} not found");

        var movement = new StockMovement
        {
            ProductId = productId,
            Quantity = quantity,
            Type = "Purchase",
            Reference = reference,
            Notes = notes,
            Timestamp = DateTime.UtcNow
        };

        product.StockQuantity += quantity;

        _context.StockMovements.Add(movement);
        await _context.SaveChangesAsync();

        return movement;
    }

    public async Task<StockMovement> RemoveStock(Guid productId, int quantity, string reference, string notes)
    {
        if (quantity <= 0)
            throw new ArgumentException("Quantity must be positive", nameof(quantity));

        var product = await _context.Products.FindAsync(productId)
            ?? throw new KeyNotFoundException($"Product with ID {productId} not found");

        if (product.StockQuantity < quantity)
            throw new InvalidOperationException("Insufficient stock");

        var movement = new StockMovement
        {
            ProductId = productId,
            Quantity = -quantity,
            Type = "Sale",
            Reference = reference,
            Notes = notes,
            Timestamp = DateTime.UtcNow
        };

        product.StockQuantity -= quantity;

        _context.StockMovements.Add(movement);
        await _context.SaveChangesAsync();

        return movement;
    }

    public async Task<StockMovement> AdjustStock(Guid productId, int newQuantity, string notes)
    {
        var product = await _context.Products.FindAsync(productId)
            ?? throw new KeyNotFoundException($"Product with ID {productId} not found");

        var difference = newQuantity - product.StockQuantity;

        var movement = new StockMovement
        {
            ProductId = productId,
            Quantity = difference,
            Type = "Adjustment",
            Reference = "Manual Adjustment",
            Notes = notes,
            Timestamp = DateTime.UtcNow
        };

        product.StockQuantity = newQuantity;

        _context.StockMovements.Add(movement);
        await _context.SaveChangesAsync();

        return movement;
    }

    public async Task<IEnumerable<StockMovement>> GetStockMovements(
        Guid productId,
        DateTime? startDate = null,
        DateTime? endDate = null)
    {
        var query = _context.StockMovements
            .Where(m => m.ProductId == productId);

        if (startDate.HasValue)
            query = query.Where(m => m.Timestamp >= startDate.Value);

        if (endDate.HasValue)
            query = query.Where(m => m.Timestamp <= endDate.Value);

        return await query
            .OrderByDescending(m => m.Timestamp)
            .ToListAsync();
    }

    public async Task<int> GetCurrentStock(Guid productId)
    {
        var product = await _context.Products.FindAsync(productId)
            ?? throw new KeyNotFoundException($"Product with ID {productId} not found");

        return product.StockQuantity;
    }

    public async Task<IEnumerable<Product>> GetLowStockProducts(int threshold = 10)
    {
        return await _context.Products
            .Where(p => p.StockQuantity <= threshold)
            .OrderBy(p => p.StockQuantity)
            .ToListAsync();
    }

    public async Task<PurchaseOrder> CreatePurchaseOrder(PurchaseOrder order)
    {
        order.OrderNumber = await GenerateOrderNumber();
        order.Status = "Draft";
        order.OrderDate = DateTime.UtcNow;

        _context.PurchaseOrders.Add(order);
        await _context.SaveChangesAsync();

        return order;
    }

    public async Task<bool> UpdatePurchaseOrderStatus(int orderId, string status)
    {
        var order = await _context.PurchaseOrders.FindAsync(orderId)
            ?? throw new KeyNotFoundException($"Purchase order with ID {orderId} not found");

        order.Status = status;
        await _context.SaveChangesAsync();

        return true;
    }

    public async Task<bool> ReceivePurchaseOrder(int orderId, IEnumerable<(Guid ProductId, int ReceivedQuantity)> receivedItems)
    {
        var order = await _context.PurchaseOrders
            .Include(po => po.Items)
            .FirstOrDefaultAsync(po => po.Id == orderId)
            ?? throw new KeyNotFoundException($"Purchase order with ID {orderId} not found");

        foreach (var (productId, receivedQuantity) in receivedItems)
        {
            var orderItem = order.Items.FirstOrDefault(i => i.ProductId == productId)
                ?? throw new InvalidOperationException($"Product {productId} not found in order");

            await AddStock(
                productId,
                receivedQuantity,
                $"PO-{order.OrderNumber}",
                $"Received from PO {order.OrderNumber}"
            );
        }

        order.Status = "Received";
        await _context.SaveChangesAsync();

        return true;
    }

    private async Task<string> GenerateOrderNumber()
    {
        var lastOrder = await _context.PurchaseOrders
            .OrderByDescending(po => po.OrderNumber)
            .FirstOrDefaultAsync();

        if (lastOrder == null)
            return "PO-0001";

        var lastNumber = int.Parse(lastOrder.OrderNumber.Split('-')[1]);
        return $"PO-{(lastNumber + 1):D4}";
    }
}