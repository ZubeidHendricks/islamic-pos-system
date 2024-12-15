using IslamicPOS.Domain.Entities;
using IslamicPOS.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace IslamicPOS.Infrastructure.Repositories;

public class SaleRepository : ISaleRepository
{
    private readonly ApplicationDbContext _context;

    public SaleRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Sale?> GetByIdAsync(int id)
    {
        return await _context.Sales
            .Include(s => s.Items)
            .FirstOrDefaultAsync(s => s.Id == id);
    }

    public async Task<IEnumerable<Sale>> GetAllAsync()
    {
        return await _context.Sales
            .Include(s => s.Items)
            .ToListAsync();
    }

    public async Task<Sale> AddAsync(Sale sale)
    {
        await _context.Sales.AddAsync(sale);
        await _context.SaveChangesAsync();
        return sale;
    }

    public async Task UpdateAsync(Sale sale)
    {
        _context.Entry(sale).State = EntityState.Modified;
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var sale = await _context.Sales.FindAsync(id);
        if (sale != null)
        {
            _context.Sales.Remove(sale);
            await _context.SaveChangesAsync();
        }
    }
}
