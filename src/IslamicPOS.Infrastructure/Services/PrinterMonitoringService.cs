using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using IslamicPOS.Infrastructure.Data;

namespace IslamicPOS.Infrastructure.Services
{
    public class PrinterMonitoringService : BackgroundService
    {
        private readonly IServiceProvider _services;
        private readonly ILogger<PrinterMonitoringService> _logger;
        private readonly TimeSpan _checkInterval = TimeSpan.FromMinutes(5);

        public PrinterMonitoringService(
            IServiceProvider services,
            ILogger<PrinterMonitoringService> logger)
        {
            _services = services;
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    await MonitorPrinters(stoppingToken);
                    await Task.Delay(_checkInterval, stoppingToken);
                }
                catch (Exception ex) when (ex is not OperationCanceledException)
                {
                    _logger.LogError(ex, "Error in printer monitoring loop");
                    await Task.Delay(TimeSpan.FromMinutes(1), stoppingToken);
                }
            }
        }

        private async Task MonitorPrinters(CancellationToken stoppingToken)
        {
            using var scope = _services.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
            var printerService = scope.ServiceProvider.GetRequiredService<PrinterIntegrationService>();

            var printers = await context.PrinterConfigurations.ToListAsync(stoppingToken);

            foreach (var printer in printers)
            {
                if (stoppingToken.IsCancellationRequested) break;

                try
                {
                    var status = await printerService.GetPrinterStatus(printer.PrinterName);
                    var statusChanged = status.Status != printer.LastStatus;

                    printer.IsOnline = status.IsOnline;
                    printer.LastStatus = status.Status;
                    printer.LastStatusCheck = DateTime.UtcNow;

                    if (statusChanged)
                    {
                        context.PrinterStatusLogs.Add(new PrinterStatusLogEntity
                        {
                            PrinterConfigurationId = printer.Id,
                            Timestamp = DateTime.UtcNow,
                            Status = status.Status,
                            ErrorMessage = !status.IsOnline ? status.Status : null
                        });

                        _logger.LogInformation(
                            $"Printer {printer.PrinterName} status changed to {status.Status}");
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, $"Error monitoring printer {printer.PrinterName}");
                    printer.IsOnline = false;
                    printer.LastStatus = "Error: " + ex.Message;
                    printer.LastStatusCheck = DateTime.UtcNow;
                    printer.PrintErrorCount++;

                    context.PrinterStatusLogs.Add(new PrinterStatusLogEntity
                    {
                        PrinterConfigurationId = printer.Id,
                        Timestamp = DateTime.UtcNow,
                        Status = "Error",
                        ErrorMessage = ex.Message
                    });
                }
            }

            await context.SaveChangesAsync(stoppingToken);
        }
    }
}