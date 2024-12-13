using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using IslamicPOS.Core.Services;
using IslamicPOS.Core.Models;

namespace IslamicPOS.Infrastructure.Services
{
    public class ReceiptService : IReceiptService
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger<ReceiptService> _logger;
        private readonly IIslamicFinanceService _islamicFinanceService;
        private readonly PrinterIntegrationService _printerService;

        public ReceiptService(
            IConfiguration configuration,
            ILogger<ReceiptService> logger,
            IIslamicFinanceService islamicFinanceService,
            PrinterIntegrationService printerService)
        {
            _configuration = configuration;
            _logger = logger;
            _islamicFinanceService = islamicFinanceService;
            _printerService = printerService;
        }

        public async Task PrintReceipt(Transaction transaction, string printerName = null)
        {
            try
            {
                // Get printer settings
                var settings = await GetPrinterSettings(printerName);

                // Check printer status
                var status = await _printerService.GetPrinterStatus(settings.PrinterName);
                if (!status.IsOnline)
                {
                    throw new PrintException($"Printer {settings.PrinterName} is not available: {status.Status}", null);
                }

                // Generate PDF
                var pdfBytes = await GenerateReceiptPdf(transaction);

                // Send to printer
                await _printerService.Print(pdfBytes, settings);

                _logger.LogInformation($"Receipt printed successfully for transaction {transaction.Id}");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error printing receipt for transaction {transaction.Id}");
                throw;
            }
        }

        public async Task<bool> TestPrinter(string printerName)
        {
            return await _printerService.TestPrinter(printerName);
        }

        // ... Rest of the ReceiptService implementation remains the same ...
    }
}