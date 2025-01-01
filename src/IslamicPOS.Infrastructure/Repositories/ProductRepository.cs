using IslamicPOS.Infrastructure.Persistence;

namespace IslamicPOS.Infrastructure.Repositories;

public class ProductRepository : BaseRepository<Product>, IProductRepository
{
    public ProductRepository(ApplicationDbContext context) : base(context)
    {
    }

    public async Task<IEnumerable<Product>> GetHalalProductsAsync()
    {
        return await _dbSet
            .Where(p => p.IsHalal)
            .ToListAsync();
    }

    public async Task<IEnumerable<Product>> GetByCategory(string category)
    {
        return await _dbSet
            .Where(p => p.Category == category)
            .ToListAsync();
    }

    public async Task<IEnumerable<Product>> GetLowStockProducts(int threshold = 10)
    {
        return await _dbSet
            .Where(p => p.StockQuantity <= threshold)
            .ToListAsync();
    }
}