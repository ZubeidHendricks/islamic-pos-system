using IslamicPOS.Infrastructure.Persistence;

namespace IslamicPOS.Infrastructure.Repositories;

public class ProfitSharingRepository : BaseRepository<ProfitSharing>, IProfitSharingRepository
{
    public ProfitSharingRepository(ApplicationDbContext context) : base(context)
    {
    }

    public async Task<IEnumerable<ProfitSharing>> GetUndistributedProfitsAsync()
    {
        return await _dbSet
            .Where(p => !p.IsDistributed)
            .OrderBy(p => p.PeriodStart)
            .ToListAsync();
    }

    public async Task<decimal> GetTotalDistributedProfitAsync(DateTime startDate, DateTime endDate)
    {
        return await _dbSet
            .Where(p => p.IsDistributed 
                && p.PeriodStart >= startDate 
                && p.PeriodEnd <= endDate)
            .SumAsync(p => p.NetProfit);
    }

    public async Task<IEnumerable<ProfitSharing>> GetByPeriodAsync(DateTime startDate, DateTime endDate)
    {
        return await _dbSet
            .Where(p => p.PeriodStart >= startDate && p.PeriodEnd <= endDate)
            .OrderBy(p => p.PeriodStart)
            .Include(p => p.DistributionDetails)
            .ToListAsync();
    }

    public async Task<IEnumerable<ProfitDistributionDetail>> GetDistributionDetailsByProfitSharingIdAsync(Guid profitSharingId)
    {
        return await _context.ProfitDistributionDetails
            .Where(d => d.ProfitSharingId == profitSharingId)
            .Include(d => d.Partner)
            .OrderByDescending(d => d.Amount)
            .ToListAsync();
    }
}