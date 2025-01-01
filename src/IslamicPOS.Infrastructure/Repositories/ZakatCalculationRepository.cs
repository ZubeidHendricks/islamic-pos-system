using IslamicPOS.Infrastructure.Persistence;

namespace IslamicPOS.Infrastructure.Repositories;

public class ZakatCalculationRepository : BaseRepository<ZakatCalculation>, IZakatCalculationRepository
{
    public ZakatCalculationRepository(ApplicationDbContext context) : base(context)
    {
    }

    public async Task<IEnumerable<ZakatCalculation>> GetUnpaidCalculationsAsync()
    {
        return await _dbSet
            .Where(z => !z.IsPaid && z.IsEligible)
            .OrderByDescending(z => z.CalculationDate)
            .ToListAsync();
    }

    public async Task<ZakatCalculation?> GetLatestCalculationAsync()
    {
        return await _dbSet
            .OrderByDescending(z => z.CalculationDate)
            .FirstOrDefaultAsync();
    }

    public async Task<decimal> GetTotalUnpaidZakatAsync()
    {
        return await _dbSet
            .Where(z => !z.IsPaid && z.IsEligible)
            .SumAsync(z => z.ZakatAmount);
    }
}