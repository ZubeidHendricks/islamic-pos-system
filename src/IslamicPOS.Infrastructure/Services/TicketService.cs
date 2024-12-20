using IslamicPOS.Domain.Common;
using IslamicPOS.Domain.Sales;
using Microsoft.Extensions.Logging;

namespace IslamicPOS.Infrastructure.Services;

public class TicketService
{
    private readonly ILogger<TicketService> _logger;

    public TicketService(ILogger<TicketService> logger)
    {
        _logger = logger;
    }

    // Implement ticket service methods
}