using IslamicPOS.Domain.Common;
using IslamicPOS.Domain.Sales;

namespace IslamicPOS.Infrastructure.Services;

public class SubscriptionService
{
    private readonly ILogger<SubscriptionService> _logger;

    public SubscriptionService(ILogger<SubscriptionService> logger)
    {
        _logger = logger;
    }

    // Implement subscription service methods
}