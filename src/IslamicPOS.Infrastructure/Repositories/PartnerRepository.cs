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
            .OrderBy(p => p.Name)
            .ToListAsync();
    }

    public async Task<decimal> GetTotalSharePercentageAsync()
    {
        return await _dbSet
            .Where(p => p.IsActive)
            .SumAsync(p => p.SharePercentage);
    }

    public async Task<IEnumerable<Partner>> GetPartnersWithDistributionsAsync(
        DateTime startDate,
        DateTime endDate)
    {
        return await _dbSet
            .Include(p => p.Distributions
                .Where(d => d.DistributionDate >= startDate && 
                           d.DistributionDate <= endDate))
            .Where(p => p.IsActive)
            .OrderBy(p => p.Name)
            .ToListAsync();
    }
}