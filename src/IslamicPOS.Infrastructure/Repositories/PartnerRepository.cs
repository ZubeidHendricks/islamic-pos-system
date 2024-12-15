using IslamicPOS.Domain.Entities;
using IslamicPOS.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace IslamicPOS.Infrastructure.Repositories;

public class PartnerRepository : IPartnerRepository
{
    private readonly ApplicationDbContext _context;

    public PartnerRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Partner?> GetByIdAsync(int id)
    {
        return await _context.Partners
            .FirstOrDefaultAsync(p => p.Id == id);
    }
}
