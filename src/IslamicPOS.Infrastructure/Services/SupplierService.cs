using IslamicPOS.Domain.Common;
using IslamicPOS.Domain.Inventory;

namespace IslamicPOS.Infrastructure.Services;

public class SupplierService
{
    private readonly ILogger<SupplierService> _logger;

    public SupplierService(ILogger<SupplierService> logger)
    {
        _logger = logger;
    }

    // Implement supplier service methods
}