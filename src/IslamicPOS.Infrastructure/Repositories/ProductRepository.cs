using IslamicPOS.Domain.Inventory;
using IslamicPOS.Domain.Repositories;
using IslamicPOS.Infrastructure.Persistence;

namespace IslamicPOS.Infrastructure.Repositories;

public class ProductRepository : BaseRepository, IProductRepository
{
    public ProductRepository(ApplicationDbContext context) : base(context) { }

    public async Task<Product> GetByIdAsync(Guid id)
    {
        return await _context.Set<Product>().FindAsync(id);
    }

    public async Task<List<Product>> GetAllAsync()
    {
        return await _context.Set<Product>().ToListAsync();
    }

    public async Task AddAsync(Product product)
    {
        await _context.Set<Product>().AddAsync(product);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Product product)
    {
        _context.Set<Product>().Update(product);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Guid id)
    {
        var product = await GetByIdAsync(id);
        if (product != null)
        {
            _context.Set<Product>().Remove(product);
            await _context.SaveChangesAsync();
        }
    }
}