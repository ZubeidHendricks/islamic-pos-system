using IslamicPOS.Domain.Common;
using IslamicPOS.Domain.Inventory;

namespace IslamicPOS.Infrastructure.Services;

public class PurchaseOrderService
{
    private readonly ILogger<PurchaseOrderService> _logger;

    public PurchaseOrderService(ILogger<PurchaseOrderService> logger)
    {
        _logger = logger;
    }

    // Implement purchase order service methods
}