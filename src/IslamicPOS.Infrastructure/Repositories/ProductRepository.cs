using Microsoft.EntityFrameworkCore;
using IslamicPOS.Core.Interfaces;
using IslamicPOS.Core.Models;
using IslamicPOS.Infrastructure.Data;

namespace IslamicPOS.Infrastructure.Repositories;

public class ProductRepository : IProductRepository
{
    private readonly ApplicationDbContext _context;

    public ProductRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Product> CreateAsync(Product product)
    {
        _context.Products.Add(product);
        await _context.SaveChangesAsync();
        return product;
    }

    public async Task<Product?> GetByIdAsync(Guid id)
    {
        return await _context.Products.FindAsync(id);
    }

    public async Task<Product?> GetByBarcodeAsync(string barcode)
    {
        return await _context.Products
            .FirstOrDefaultAsync(p => p.Barcode == barcode);
    }

    public async Task<IEnumerable<Product>> GetAllAsync()
    {
        return await _context.Products.ToListAsync();
    }

    public async Task<IEnumerable<Product>> SearchAsync(string query)
    {
        return await _context.Products
            .Where(p => p.Name.Contains(query) || p.Barcode.Contains(query))
            .ToListAsync();
    }

    public async Task UpdateStockAsync(Guid id, int quantity)
    {
        var product = await _context.Products.FindAsync(id);
        if (product != null)
        {
            product.StockQuantity += quantity;
            await _context.SaveChangesAsync();
        }
    }
}