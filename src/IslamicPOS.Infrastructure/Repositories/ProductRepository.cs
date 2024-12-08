namespace IslamicPOS.Infrastructure.Repositories;

public class ProductRepository : BaseRepository<Product>, IProductRepository
{
    public ProductRepository(ApplicationDbContext context) : base(context)
    {
    }

    public async Task<IEnumerable<Product>> GetLowStockProductsAsync(int threshold = 10)
    {
        return await _dbSet
            .Where(p => p.StockQuantity <= threshold && p.IsActive)
            .OrderBy(p => p.StockQuantity)
            .ToListAsync();
    }

    public async Task<Product?> GetByBarcodeAsync(string barcode)
    {
        return await _dbSet
            .FirstOrDefaultAsync(p => p.Barcode == barcode && p.IsActive);
    }

    public async Task<decimal> GetTotalInventoryValueAsync()
    {
        return await _dbSet
            .Where(p => p.IsActive)
            .SumAsync(p => p.Price * p.StockQuantity);
    }

    public async Task<bool> UpdateStockAsync(int productId, int quantity)
    {
        var product = await _dbSet.FindAsync(productId);
        if (product == null) return false;

        product.StockQuantity += quantity;
        product.UpdatedAt = DateTime.UtcNow;

        await _context.SaveChangesAsync();
        return true;
    }
}