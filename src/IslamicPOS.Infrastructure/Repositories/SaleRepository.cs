using IslamicPOS.Domain.Sales;
using IslamicPOS.Domain.Common.Models;
using IslamicPOS.Domain.Repositories;
using IslamicPOS.Infrastructure.Persistence;

namespace IslamicPOS.Infrastructure.Repositories;

public class SaleRepository : BaseRepository, ISaleRepository
{
    public SaleRepository(ApplicationDbContext context) : base(context) { }

    public async Task<Sale> GetByIdAsync(Guid id)
    {
        return await _context.Set<Sale>().FindAsync(id);
    }

    public async Task<List<Sale>> GetAllAsync()
    {
        return await _context.Set<Sale>().ToListAsync();
    }

    public async Task AddAsync(Sale sale)
    {
        await _context.Set<Sale>().AddAsync(sale);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Sale sale)
    {
        _context.Set<Sale>().Update(sale);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Guid id)
    {
        var sale = await GetByIdAsync(id);
        if (sale != null)
        {
            _context.Set<Sale>().Remove(sale);
            await _context.SaveChangesAsync();
        }
    }

    public async Task<List<SalesByProduct>> GetSalesByProductAsync()
    {
        // Implement sales by product logic
        return new List<SalesByProduct>();
    }

    public async Task<SaleSummary> GetSalesSummaryAsync()
    {
        // Implement sales summary logic
        return new SaleSummary();
    }
}