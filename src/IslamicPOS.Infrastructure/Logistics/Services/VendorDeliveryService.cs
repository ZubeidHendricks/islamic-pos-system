using IslamicPOS.Domain.Common;
using IslamicPOS.Domain.Logistics;
using Microsoft.Extensions.Logging;

namespace IslamicPOS.Infrastructure.Logistics.Services;

public class VendorDeliveryService
{
    private readonly ILogger<VendorDeliveryService> _logger;
    private readonly RouteService _routeService;

    public VendorDeliveryService(ILogger<VendorDeliveryService> logger, RouteService routeService)
    {
        _logger = logger;
        _routeService = routeService;
    }

    // Implement vendor delivery service methods
}