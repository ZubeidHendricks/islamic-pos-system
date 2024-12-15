using IslamicPOS.Domain.Common;
using IslamicPOS.Application.Common.Interfaces;

namespace IslamicPOS.Infrastructure.Repositories;

public class TransactionRepository
{
    private readonly IApplicationDbContext _context;

    public TransactionRepository(IApplicationDbContext context)
    {
        _context = context;
    }

    // Implement repository methods
}