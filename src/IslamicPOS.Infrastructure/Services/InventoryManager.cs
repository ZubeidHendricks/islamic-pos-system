using IslamicPOS.Domain.Common;
using IslamicPOS.Domain.Inventory;

namespace IslamicPOS.Infrastructure.Services;

public class InventoryManager
{
    private readonly ILogger<InventoryManager> _logger;

    public InventoryManager(ILogger<InventoryManager> logger)
    {
        _logger = logger;
    }

    // Implement inventory management methods
}