using IslamicPOS.Infrastructure.Persistence;

namespace IslamicPOS.Infrastructure.Repositories;

public class PartnerRepository : BaseRepository<Partner>, IPartnerRepository
{
    public PartnerRepository(ApplicationDbContext context) : base(context)
    {
    }

    public async Task<IEnumerable<Partner>> GetActivePartnersAsync()
    {
        return await _dbSet
            .Where(p => p.IsActive)
            .OrderByDescending(p => p.InvestmentAmount)
            .ToListAsync();
    }

    public async Task<decimal> GetTotalInvestmentAsync()
    {
        return await _dbSet
            .Where(p => p.IsActive)
            .SumAsync(p => p.InvestmentAmount);
    }

    public async Task<IEnumerable<Partner>> GetPartnersByProfitShareRangeAsync(decimal minShare, decimal maxShare)
    {
        return await _dbSet
            .Where(p => p.IsActive && 
                       p.ProfitSharePercentage >= minShare && 
                       p.ProfitSharePercentage <= maxShare)
            .OrderByDescending(p => p.ProfitSharePercentage)
            .ToListAsync();
    }

    public async Task<IEnumerable<Partner>> SearchPartnersAsync(string searchTerm)
    {
        return await _dbSet
            .Where(p => p.Name.Contains(searchTerm) || 
                       p.Email.Contains(searchTerm) ||
                       p.Phone.Contains(searchTerm))
            .OrderBy(p => p.Name)
            .ToListAsync();
    }
}