using IslamicPOS.Domain.Common;
using IslamicPOS.Domain.Inventory;

namespace IslamicPOS.Infrastructure.Services;

public class InventoryService
{
    private readonly ILogger<InventoryService> _logger;

    public InventoryService(ILogger<InventoryService> logger)
    {
        _logger = logger;
    }

    // Implement inventory service methods
}