using IslamicPOS.Infrastructure.Persistence;

namespace IslamicPOS.Infrastructure.Repositories;

public abstract class BaseRepository
{
    protected readonly ApplicationDbContext _context;

    protected BaseRepository(ApplicationDbContext context)
    {
        _context = context;
    }
}