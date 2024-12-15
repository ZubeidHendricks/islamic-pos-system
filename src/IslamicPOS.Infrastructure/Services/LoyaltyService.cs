using IslamicPOS.Domain.Common;
using IslamicPOS.Domain.Sales;

namespace IslamicPOS.Infrastructure.Services;

public class LoyaltyService
{
    private readonly ILogger<LoyaltyService> _logger;

    public LoyaltyService(ILogger<LoyaltyService> logger)
    {
        _logger = logger;
    }

    // Implement loyalty service methods
}