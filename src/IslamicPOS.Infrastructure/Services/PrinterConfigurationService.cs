using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using IslamicPOS.Core.Services;
using IslamicPOS.Infrastructure.Data;
using IslamicPOS.Infrastructure.Data.Models;

namespace IslamicPOS.Infrastructure.Services
{
    public class PrinterConfigurationService
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<PrinterConfigurationService> _logger;

        public PrinterConfigurationService(
            ApplicationDbContext context,
            ILogger<PrinterConfigurationService> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<PrinterSettings> GetPrinterSettings(string printerName = null)
        {
            try
            {
                PrinterConfigurationEntity config;

                if (string.IsNullOrEmpty(printerName))
                {
                    // Get default printer configuration
                    config = await _context.PrinterConfigurations
                        .FirstOrDefaultAsync(p => p.IsDefault)
                        ?? await _context.PrinterConfigurations.FirstOrDefaultAsync()
                        ?? CreateDefaultConfiguration();
                }
                else
                {
                    // Get specific printer configuration
                    config = await _context.PrinterConfigurations
                        .FirstOrDefaultAsync(p => p.PrinterName == printerName)
                        ?? CreateDefaultConfiguration(printerName);
                }

                return MapToSettings(config);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving printer settings");
                throw;
            }
        }

        public async Task SavePrinterSettings(PrinterSettings settings)
        {
            try
            {
                var config = await _context.PrinterConfigurations
                    .FirstOrDefaultAsync(p => p.PrinterName == settings.PrinterName);

                if (config == null)
                {
                    config = new PrinterConfigurationEntity
                    {
                        PrinterName = settings.PrinterName
                    };
                    _context.PrinterConfigurations.Add(config);
                }

                // Update configuration
                UpdateConfiguration(config, settings);

                // If this is set as default, unset others
                if (settings.IsDefault)
                {
                    var otherConfigs = await _context.PrinterConfigurations
                        .Where(p => p.PrinterName != settings.PrinterName)
                        .ToListAsync();

                    foreach (var other in otherConfigs)
                    {
                        other.IsDefault = false;
                    }
                }

                await _context.SaveChangesAsync();
                _logger.LogInformation($"Printer settings saved for {settings.PrinterName}");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error saving printer settings for {settings.PrinterName}");
                throw;
            }
        }

        private PrinterConfigurationEntity CreateDefaultConfiguration(string printerName = null)
        {
            return new PrinterConfigurationEntity
            {
                PrinterName = printerName ?? "Default Printer",
                IsDefault = true,
                PaperSize = "80mm",
                Copies = 1,
                PrintLogo = true,
                PrintHalalCertification = true,
                HeaderText = "Islamic POS System",
                FooterText = "Thank you for your business\nMay Allah bless your purchase",
                LastUpdated = DateTime.UtcNow
            };
        }

        private void UpdateConfiguration(PrinterConfigurationEntity config, PrinterSettings settings)
        {
            config.IsDefault = settings.IsDefault;
            config.PaperSize = settings.PaperSize;
            config.Copies = settings.Copies;
            config.PrintLogo = settings.PrintLogo;
            config.PrintHalalCertification = settings.PrintHalalCertification;
            config.HeaderText = settings.HeaderText;
            config.FooterText = settings.FooterText;
            config.LastUpdated = DateTime.UtcNow;
        }

        private PrinterSettings MapToSettings(PrinterConfigurationEntity config)
        {
            return new PrinterSettings
            {
                PrinterName = config.PrinterName,
                IsDefault = config.IsDefault,
                PaperSize = config.PaperSize,
                Copies = config.Copies,
                PrintLogo = config.PrintLogo,
                PrintHalalCertification = config.PrintHalalCertification,
                HeaderText = config.HeaderText,
                FooterText = config.FooterText
            };
        }
    }
}