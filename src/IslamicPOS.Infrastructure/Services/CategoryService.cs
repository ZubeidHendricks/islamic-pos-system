using IslamicPOS.Domain.Common;
using IslamicPOS.Domain.Inventory;

namespace IslamicPOS.Infrastructure.Services;

public class CategoryService
{
    private readonly ILogger<CategoryService> _logger;

    public CategoryService(ILogger<CategoryService> logger)
    {
        _logger = logger;
    }

    // Implement category service methods
}