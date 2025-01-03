using IslamicPOS.Core.Models.IslamicFinance;
using IslamicPOS.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace IslamicPOS.Infrastructure.Services;

public class ProfitSharingService
{
    private readonly ApplicationDbContext _context;

    public ProfitSharingService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<ProfitSharing> GetByIdAsync(Guid id)
    {
        return await _context.ProfitSharings
            .Include(p => p.DistributionDetails)
            .FirstOrDefaultAsync(p => p.Id == id);
    }

    public async Task<List<ProfitSharing>> GetAllAsync()
    {
        return await _context.ProfitSharings
            .Include(p => p.DistributionDetails)
            .Where(p => !p.IsDeleted)
            .ToListAsync();
    }

    public async Task<ProfitSharing> CreateAsync(ProfitSharing profitSharing)
    {
        _context.ProfitSharings.Add(profitSharing);
        await _context.SaveChangesAsync();
        return profitSharing;
    }

    public async Task<ProfitSharing> UpdateAsync(ProfitSharing profitSharing)
    {
        _context.Update(profitSharing);
        await _context.SaveChangesAsync();
        return profitSharing;
    }

    public async Task DeleteAsync(Guid id)
    {
        var profitSharing = await GetByIdAsync(id);
        if (profitSharing != null)
        {
            profitSharing.IsDeleted = true;
            await _context.SaveChangesAsync();
        }
    }
}