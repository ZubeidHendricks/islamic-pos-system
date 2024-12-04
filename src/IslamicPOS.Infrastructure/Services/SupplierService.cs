using Microsoft.EntityFrameworkCore;
using IslamicPOS.Core.Models;
using IslamicPOS.Core.Models.InventoryManagement;
using IslamicPOS.Core.Services;
using IslamicPOS.Infrastructure.Data;

namespace IslamicPOS.Infrastructure.Services;

public class SupplierService : ISupplierService
{
    private readonly ApplicationDbContext _context;
    private readonly ILogger<SupplierService> _logger;

    public SupplierService(ApplicationDbContext context, ILogger<SupplierService> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<Supplier> CreateSupplier(Supplier supplier)
    {
        supplier.CreatedAt = DateTime.UtcNow;
        _context.Suppliers.Add(supplier);
        await _context.SaveChangesAsync();
        return supplier;
    }

    public async Task<Supplier> UpdateSupplier(Supplier supplier)
    {
        var existing = await _context.Suppliers.FindAsync(supplier.Id)
            ?? throw new KeyNotFoundException($"Supplier {supplier.Id} not found");

        existing.Name = supplier.Name;
        existing.ContactPerson = supplier.ContactPerson;
        existing.Email = supplier.Email;
        existing.Phone = supplier.Phone;
        existing.Address = supplier.Address;
        existing.PaymentTerms = supplier.PaymentTerms;
        existing.IsActive = supplier.IsActive;

        await _context.SaveChangesAsync();
        return existing;
    }

    public async Task<bool> DeleteSupplier(int id)
    {
        var supplier = await _context.Suppliers.FindAsync(id)
            ?? throw new KeyNotFoundException($"Supplier {id} not found");

        supplier.IsActive = false;
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<Supplier> GetSupplierById(int id)
    {
        return await _context.Suppliers
            .Include(s => s.PurchaseOrders)
            .FirstOrDefaultAsync(s => s.Id == id)
            ?? throw new KeyNotFoundException($"Supplier {id} not found");
    }

    public async Task<List<Supplier>> GetAllSuppliers()
    {
        return await _context.Suppliers
            .OrderBy(s => s.Name)
            .ToListAsync();
    }

    public async Task<List<Supplier>> SearchSuppliers(string query)
    {
        return await _context.Suppliers
            .Where(s => s.Name.Contains(query) ||
                       s.ContactPerson.Contains(query) ||
                       s.Email.Contains(query))
            .OrderBy(s => s.Name)
            .ToListAsync();
    }

    public async Task<List<Supplier>> GetActiveSuppliers()
    {
        return await _context.Suppliers
            .Where(s => s.IsActive)
            .OrderBy(s => s.Name)
            .ToListAsync();
    }

    public async Task<List<SupplierPerformance>> GetSupplierPerformance(DateTime startDate, DateTime endDate)
    {
        var suppliers = await _context.Suppliers
            .Where(s => s.IsActive)
            .Include(s => s.PurchaseOrders)
            .ToListAsync();

        var performance = new List<SupplierPerformance>();

        foreach (var supplier in suppliers)
        {
            var orders = supplier.PurchaseOrders
                .Where(po => po.OrderDate >= startDate && 
                            po.OrderDate <= endDate &&
                            po.Status != "Cancelled")
                .ToList();

            if (!orders.Any()) continue;

            var onTimeDeliveries = orders.Count(o => 
                o.Status == "Received" && 
                o.ExpectedDeliveryDate.HasValue &&
                o.OrderDate <= o.ExpectedDeliveryDate.Value);

            var lateDeliveries = orders.Count(o =>
                o.Status == "Received" &&
                o.ExpectedDeliveryDate.HasValue &&
                o.OrderDate > o.ExpectedDeliveryDate.Value);

            var returnedOrders = orders.Count(o => o.Status == "Returned");

            performance.Add(new SupplierPerformance
            {
                SupplierId = supplier.Id,
                SupplierName = supplier.Name,
                TotalOrders = orders.Count,
                TotalValue = orders.Sum(o => o.TotalAmount),
                OnTimeDeliveries = onTimeDeliveries,
                LateDeliveries = lateDeliveries,
                ReturnedOrders = returnedOrders
            });
        }

        return performance;
    }
}