using IslamicPOS.Domain.Common;
using IslamicPOS.Domain.Sales;

namespace IslamicPOS.Infrastructure.Services;

public class ReportingService
{
    private readonly ILogger<ReportingService> _logger;

    public ReportingService(ILogger<ReportingService> logger)
    {
        _logger = logger;
    }

    // Implement reporting service methods
}