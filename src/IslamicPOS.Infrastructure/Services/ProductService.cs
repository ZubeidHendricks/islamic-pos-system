using IslamicPOS.Domain.Common;
using IslamicPOS.Domain.Inventory;

namespace IslamicPOS.Infrastructure.Services;

public class ProductService
{
    private readonly ILogger<ProductService> _logger;

    public ProductService(ILogger<ProductService> logger)
    {
        _logger = logger;
    }

    // Implement product service methods
}