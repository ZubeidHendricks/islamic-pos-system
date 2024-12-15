using IslamicPOS.Domain.Common;
using IslamicPOS.Domain.Logistics;

namespace IslamicPOS.Infrastructure.Services.Logistics;

public class FleetManagementService
{
    private readonly ILogger<FleetManagementService> _logger;

    public FleetManagementService(ILogger<FleetManagementService> logger)
    {
        _logger = logger;
    }

    // Implement fleet management methods
}