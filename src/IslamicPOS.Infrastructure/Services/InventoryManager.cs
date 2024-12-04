using Microsoft.EntityFrameworkCore;
using IslamicPOS.Core.Models;
using IslamicPOS.Core.Services;
using IslamicPOS.Infrastructure.Data;

namespace IslamicPOS.Infrastructure.Services;

public class InventoryManager : IInventoryManager
{
    private readonly ApplicationDbContext _context;
    private readonly ILogger<InventoryManager> _logger;

    public InventoryManager(ApplicationDbContext context, ILogger<InventoryManager> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<bool> ProcessSale(Transaction transaction)
    {
        using var transaction = await _context.Database.BeginTransactionAsync();
        try
        {
            foreach (var item in transaction.Items)
            {
                var product = await _context.Products.FindAsync(item.ProductId)
                    ?? throw new KeyNotFoundException($"Product {item.ProductId} not found");

                if (product.StockQuantity < item.Quantity)
                    throw new InvalidOperationException($"Insufficient stock for {product.Name}");

                product.StockQuantity -= item.Quantity;

                var adjustment = new StockAdjustment
                {
                    ProductId = product.Id,
                    ProductName = product.Name,
                    QuantityBefore = product.StockQuantity + item.Quantity,
                    QuantityAfter = product.StockQuantity,
                    Reason = $"Sale - Transaction {transaction.Id}",
                    Timestamp = DateTime.UtcNow
                };

                _context.StockAdjustments.Add(adjustment);
            }

            await _context.SaveChangesAsync();
            await transaction.CommitAsync();
            return true;
        }
        catch (Exception ex)
        {
            await transaction.RollbackAsync();
            _logger.LogError(ex, "Error processing sale transaction {TransactionId}", transaction.Id);
            throw;
        }
    }

    public async Task<bool> ProcessReturn(Transaction transaction)
    {
        using var transaction = await _context.Database.BeginTransactionAsync();
        try
        {
            foreach (var item in transaction.Items)
            {
                var product = await _context.Products.FindAsync(item.ProductId)
                    ?? throw new KeyNotFoundException($"Product {item.ProductId} not found");

                product.StockQuantity += item.Quantity;

                var adjustment = new StockAdjustment
                {
                    ProductId = product.Id,
                    ProductName = product.Name,
                    QuantityBefore = product.StockQuantity - item.Quantity,
                    QuantityAfter = product.StockQuantity,
                    Reason = $"Return - Transaction {transaction.Id}",
                    Timestamp = DateTime.UtcNow
                };

                _context.StockAdjustments.Add(adjustment);
            }

            await _context.SaveChangesAsync();
            await transaction.CommitAsync();
            return true;
        }
        catch (Exception ex)
        {
            await transaction.RollbackAsync();
            _logger.LogError(ex, "Error processing return transaction {TransactionId}", transaction.Id);
            throw;
        }
    }

    public async Task<bool> AdjustStock(Guid productId, int adjustment, string reason)
    {
        var product = await _context.Products.FindAsync(productId)
            ?? throw new KeyNotFoundException($"Product {productId} not found");

        var newQuantity = product.StockQuantity + adjustment;
        if (newQuantity < 0)
            throw new InvalidOperationException("Stock adjustment would result in negative quantity");

        var stockAdjustment = new StockAdjustment
        {
            ProductId = product.Id,
            ProductName = product.Name,
            QuantityBefore = product.StockQuantity,
            QuantityAfter = newQuantity,
            Reason = reason,
            Timestamp = DateTime.UtcNow
        };

        product.StockQuantity = newQuantity;
        _context.StockAdjustments.Add(stockAdjustment);
        await _context.SaveChangesAsync();

        return true;
    }

    public async Task<StockAdjustment> GetStockAdjustmentHistory(Guid productId)
    {
        return await _context.StockAdjustments
            .Where(sa => sa.ProductId == productId)
            .OrderByDescending(sa => sa.Timestamp)
            .ToListAsync();
    }

    public async Task<List<StockAlert>> GetLowStockAlerts(int threshold = 10)
    {
        var products = await _context.Products
            .Where(p => p.StockQuantity <= threshold)
            .ToListAsync();

        var alerts = new List<StockAlert>();
        foreach (var product in products)
        {
            // Calculate average daily sales for the last 30 days
            var endDate = DateTime.UtcNow;
            var startDate = endDate.AddDays(-30);
            var totalSales = await _context.TransactionItems
                .Where(ti => ti.ProductId == product.Id &&
                            ti.Transaction.Timestamp >= startDate &&
                            ti.Transaction.Timestamp <= endDate)
                .SumAsync(ti => ti.Quantity);

            var avgDailySales = totalSales / 30.0;

            alerts.Add(new StockAlert
            {
                ProductId = product.Id,
                ProductName = product.Name,
                Category = product.Category,
                CurrentStock = product.StockQuantity,
                MinimumStock = threshold,
                ReorderPoint = (int)(avgDailySales * 7), // 7 days of stock
                LastRestocked = await GetLastRestockDate(product.Id),
                AverageDailySales = (int)Math.Ceiling(avgDailySales)
            });
        }

        return alerts;
    }

    private async Task<DateTime> GetLastRestockDate(Guid productId)
    {
        var lastRestock = await _context.StockAdjustments
            .Where(sa => sa.ProductId == productId && sa.Adjustment > 0)
            .OrderByDescending(sa => sa.Timestamp)
            .FirstOrDefaultAsync();

        return lastRestock?.Timestamp ?? DateTime.MinValue;
    }

    public async Task<bool> ValidateStock(List<TransactionItem> items)
    {
        foreach (var item in items)
        {
            var product = await _context.Products.FindAsync(item.ProductId)
                ?? throw new KeyNotFoundException($"Product {item.ProductId} not found");

            if (product.StockQuantity < item.Quantity)
                return false;
        }

        return true;
    }

    public async Task<decimal> GetInventoryValue()
    {
        return await _context.Products
            .SumAsync(p => p.Price * p.StockQuantity);
    }
}