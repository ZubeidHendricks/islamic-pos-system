using Microsoft.EntityFrameworkCore;
using IslamicPOS.Core.Models.InventoryManagement;
using IslamicPOS.Core.Services;
using IslamicPOS.Infrastructure.Data;

namespace IslamicPOS.Infrastructure.Services;

public class PurchaseOrderService : IPurchaseOrderService
{
    private readonly ApplicationDbContext _context;
    private readonly ILogger<PurchaseOrderService> _logger;

    public PurchaseOrderService(ApplicationDbContext context, ILogger<PurchaseOrderService> logger)
    {
        _context = context;
        _logger = logger;
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

    public async Task<PurchaseOrder> UpdatePurchaseOrder(PurchaseOrder order)
    {
        var existing = await _context.PurchaseOrders
            .Include(po => po.Items)
            .FirstOrDefaultAsync(po => po.Id == order.Id)
            ?? throw new KeyNotFoundException($"Order {order.Id} not found");

        if (existing.Status != "Draft")
            throw new InvalidOperationException("Only draft orders can be updated");

        existing.SupplierId = order.SupplierId;
        existing.ExpectedDeliveryDate = order.ExpectedDeliveryDate;
        existing.Notes = order.Notes;

        // Update items
        _context.PurchaseOrderItems.RemoveRange(existing.Items);
        existing.Items = order.Items;
        existing.TotalAmount = order.Items.Sum(i => i.Quantity * i.UnitCost);

        await _context.SaveChangesAsync();
        return existing;
    }

    public async Task<bool> DeletePurchaseOrder(int id)
    {
        var order = await _context.PurchaseOrders.FindAsync(id)
            ?? throw new KeyNotFoundException($"Order {id} not found");

        if (order.Status != "Draft")
            throw new InvalidOperationException("Only draft orders can be deleted");

        _context.PurchaseOrders.Remove(order);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<PurchaseOrder> GetPurchaseOrderById(int id)
    {
        return await _context.PurchaseOrders
            .Include(po => po.Supplier)
            .Include(po => po.Items)
                .ThenInclude(i => i.Product)
            .FirstOrDefaultAsync(po => po.Id == id)
            ?? throw new KeyNotFoundException($"Order {id} not found");
    }

    public async Task<List<PurchaseOrder>> GetPurchaseOrders(DateTime? startDate = null, DateTime? endDate = null)
    {
        var query = _context.PurchaseOrders
            .Include(po => po.Supplier)
            .Include(po => po.Items)
                .ThenInclude(i => i.Product)
            .AsQueryable();

        if (startDate.HasValue)
            query = query.Where(po => po.OrderDate >= startDate.Value);

        if (endDate.HasValue)
            query = query.Where(po => po.OrderDate <= endDate.Value);

        return await query
            .OrderByDescending(po => po.OrderDate)
            .ToListAsync();
    }

    public async Task<List<PurchaseOrder>> GetPurchaseOrdersByStatus(string status)
    {
        return await _context.PurchaseOrders
            .Include(po => po.Supplier)
            .Include(po => po.Items)
                .ThenInclude(i => i.Product)
            .Where(po => po.Status == status)
            .OrderByDescending(po => po.OrderDate)
            .ToListAsync();
    }

    public async Task<bool> UpdateOrderStatus(int orderId, string status)
    {
        var order = await _context.PurchaseOrders.FindAsync(orderId)
            ?? throw new KeyNotFoundException($"Order {orderId} not found");

        order.Status = status;
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<decimal> GetTotalOrdersValue(DateTime startDate, DateTime endDate)
    {
        return await _context.PurchaseOrders
            .Where(po => po.OrderDate >= startDate && 
                        po.OrderDate <= endDate &&
                        po.Status != "Cancelled")
            .SumAsync(po => po.TotalAmount);
    }

    public async Task<List<PurchaseOrder>> GetSupplierOrders(int supplierId)
    {
        return await _context.PurchaseOrders
            .Include(po => po.Items)
                .ThenInclude(i => i.Product)
            .Where(po => po.SupplierId == supplierId)
            .OrderByDescending(po => po.OrderDate)
            .ToListAsync();
    }

    private async Task<string> GenerateOrderNumber()
    {
        var lastOrder = await _context.PurchaseOrders
            .OrderByDescending(po => po.OrderNumber)
            .FirstOrDefaultAsync();

        if (lastOrder == null)
            return "PO-0001";

        var number = int.Parse(lastOrder.OrderNumber.Split('-')[1]);
        return $"PO-{(number + 1):D4}";
    }
}