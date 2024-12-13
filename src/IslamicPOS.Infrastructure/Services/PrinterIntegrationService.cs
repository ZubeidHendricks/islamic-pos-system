using System.Drawing.Printing;
using Microsoft.Extensions.Logging;
using IslamicPOS.Core.Services;

namespace IslamicPOS.Infrastructure.Services
{
    public class PrinterIntegrationService
    {
        private readonly ILogger<PrinterIntegrationService> _logger;

        public PrinterIntegrationService(ILogger<PrinterIntegrationService> logger)
        {
            _logger = logger;
        }

        public async Task Print(byte[] document, PrinterSettings settings)
        {
            try
            {
                using var printDocument = new PrintDocument();
                printDocument.PrinterSettings.PrinterName = settings.PrinterName;
                printDocument.DefaultPageSettings.Copies = (short)settings.Copies;

                // Set up print handler
                printDocument.PrintPage += (sender, e) =>
                {
                    try
                    {
                        // Convert byte array to image for printing
                        using var ms = new MemoryStream(document);
                        using var image = Image.FromStream(ms);

                        // Calculate dimensions based on paper size
                        var paperWidth = e.PageBounds.Width;
                        var paperHeight = e.PageBounds.Height;
                        var ratio = Math.Min(paperWidth / image.Width, paperHeight / image.Height);

                        var newWidth = (int)(image.Width * ratio);
                        var newHeight = (int)(image.Height * ratio);

                        // Center the image
                        var x = (paperWidth - newWidth) / 2;
                        var y = (paperHeight - newHeight) / 2;

                        e.Graphics.DrawImage(image, x, y, newWidth, newHeight);
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError(ex, "Error during print page handling");
                        throw;
                    }
                };

                // Start printing
                printDocument.Print();

                _logger.LogInformation($"Document sent to printer {settings.PrinterName}");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error printing to {settings.PrinterName}");
                throw new PrintException($"Failed to print to {settings.PrinterName}", ex);
            }
        }

        public async Task<bool> TestPrinter(string printerName)
        {
            try
            {
                var printDocument = new PrintDocument
                {
                    PrinterSettings = { PrinterName = printerName }
                };

                return printDocument.PrinterSettings.IsValid;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error testing printer {printerName}");
                return false;
            }
        }

        public async Task<PrinterStatus> GetPrinterStatus(string printerName)
        {
            try
            {
                var printDocument = new PrintDocument
                {
                    PrinterSettings = { PrinterName = printerName }
                };

                if (!printDocument.PrinterSettings.IsValid)
                {
                    return new PrinterStatus
                    {
                        IsOnline = false,
                        Status = "Printer not found or invalid"
                    };
                }

                return new PrinterStatus
                {
                    IsOnline = true,
                    Status = "Ready",
                    IsPaperLow = false // Would need specific printer API to check this
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error getting status for printer {printerName}");
                return new PrinterStatus
                {
                    IsOnline = false,
                    Status = ex.Message
                };
            }
        }
    }

    public class PrinterStatus
    {
        public bool IsOnline { get; set; }
        public string Status { get; set; }
        public bool IsPaperLow { get; set; }
    }

    public class PrintException : Exception
    {
        public PrintException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}