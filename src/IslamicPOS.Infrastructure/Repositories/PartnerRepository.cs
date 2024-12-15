using IslamicPOS.Domain.Finance;
using IslamicPOS.Domain.Repositories;
using IslamicPOS.Infrastructure.Persistence;

namespace IslamicPOS.Infrastructure.Repositories;

public class PartnerRepository : BaseRepository, IPartnerRepository
{
    public PartnerRepository(ApplicationDbContext context) : base(context) { }

    public async Task<Partner> GetByIdAsync(Guid id)
    {
        return await _context.Set<Partner>().FindAsync(id);
    }

    public async Task<List<Partner>> GetAllAsync()
    {
        return await _context.Set<Partner>().ToListAsync();
    }

    public async Task AddAsync(Partner partner)
    {
        await _context.Set<Partner>().AddAsync(partner);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Partner partner)
    {
        _context.Set<Partner>().Update(partner);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Guid id)
    {
        var partner = await GetByIdAsync(id);
        if (partner != null)
        {
            _context.Set<Partner>().Remove(partner);
            await _context.SaveChangesAsync();
        }
    }
}