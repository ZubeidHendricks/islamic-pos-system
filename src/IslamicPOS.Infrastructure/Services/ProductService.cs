using Microsoft.EntityFrameworkCore;
using IslamicPOS.Core.Models;
using IslamicPOS.Core.Services;
using IslamicPOS.Infrastructure.Data;
using System.Text;

namespace IslamicPOS.Infrastructure.Services;

public class ProductService : IProductService
{
    private readonly ApplicationDbContext _context;
    private readonly ILogger<ProductService> _logger;

    public ProductService(ApplicationDbContext context, ILogger<ProductService> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<Product> CreateProduct(Product product)
    {
        if (product.Barcode != null)
        {
            var exists = await _context.Products
                .AnyAsync(p => p.Barcode == product.Barcode);
                
            if (exists)
            {
                throw new InvalidOperationException("Product with this barcode already exists");
            }
        }

        _context.Products.Add(product);
        await _context.SaveChangesAsync();
        return product;
    }

    public async Task<Product> UpdateProduct(Product product)
    {
        var existing = await GetProductById(product.Id);

        // Check barcode uniqueness if changed
        if (product.Barcode != existing.Barcode && product.Barcode != null)
        {
            var barcodeExists = await _context.Products
                .AnyAsync(p => p.Barcode == product.Barcode && p.Id != product.Id);
                
            if (barcodeExists)
            {
                throw new InvalidOperationException("Product with this barcode already exists");
            }
        }

        existing.Name = product.Name;
        existing.Description = product.Description;
        existing.Price = product.Price;
        existing.Barcode = product.Barcode;
        existing.Category = product.Category;
        existing.IsZakaatable = product.IsZakaatable;

        await _context.SaveChangesAsync();
        return existing;
    }

    public async Task<bool> DeleteProduct(Guid id)
    {
        var product = await GetProductById(id);
        _context.Products.Remove(product);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<Product> GetProductById(Guid id)
    {
        return await _context.Products
            .FirstOrDefaultAsync(p => p.Id == id)
            ?? throw new KeyNotFoundException($"Product {id} not found");
    }

    public async Task<Product> GetProductByBarcode(string barcode)
    {
        return await _context.Products
            .FirstOrDefaultAsync(p => p.Barcode == barcode);
    }

    public async Task<List<Product>> GetProductsByCategory(int categoryId)
    {
        return await _context.Products
            .Where(p => p.Category == categoryId.ToString())
            .OrderBy(p => p.Name)
            .ToListAsync();
    }

    public async Task<List<Product>> SearchProducts(string query)
    {
        if (string.IsNullOrWhiteSpace(query))
            return await _context.Products.Take(20).ToListAsync();

        return await _context.Products
            .Where(p => p.Name.Contains(query) || 
                       p.Barcode.Contains(query) ||
                       p.Description.Contains(query))
            .OrderBy(p => p.Name)
            .Take(20)
            .ToListAsync();
    }

    public async Task<bool> UpdateStock(Guid id, int quantity)
    {
        var product = await GetProductById(id);
        product.StockQuantity = quantity;
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<List<Product>> GetLowStockProducts(int threshold = 10)
    {
        return await _context.Products
            .Where(p => p.StockQuantity <= threshold)
            .OrderBy(p => p.StockQuantity)
            .ToListAsync();
    }

    public async Task<bool> ImportProducts(List<Product> products)
    {
        try
        {
            foreach (var product in products)
            {
                if (product.Barcode != null)
                {
                    var existing = await GetProductByBarcode(product.Barcode);
                    if (existing != null)
                    {
                        // Update existing product
                        existing.Name = product.Name;
                        existing.Description = product.Description;
                        existing.Price = product.Price;
                        existing.StockQuantity = product.StockQuantity;
                        existing.Category = product.Category;
                        existing.IsZakaatable = product.IsZakaatable;
                    }
                    else
                    {
                        // Add new product
                        _context.Products.Add(product);
                    }
                }
                else
                {
                    // Add new product without barcode
                    _context.Products.Add(product);
                }
            }

            await _context.SaveChangesAsync();
            return true;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error importing products");
            throw;
        }
    }

    public async Task<byte[]> ExportProductsCsv()
    {
        var products = await _context.Products
            .OrderBy(p => p.Name)
            .ToListAsync();

        var csv = new StringBuilder();
        csv.AppendLine("Id,Name,Description,Barcode,Price,StockQuantity,Category,IsZakaatable");

        foreach (var product in products)
        {
            csv.AppendLine($"\"{product.Id}\",\"{product.Name}\",\"{product.Description}\",\"{product.Barcode}\",{product.Price},{product.StockQuantity},\"{product.Category}\",{product.IsZakaatable}");
        }

        return Encoding.UTF8.GetBytes(csv.ToString());
    }
}