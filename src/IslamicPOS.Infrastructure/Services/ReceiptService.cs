using System.Drawing.Printing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using IslamicPOS.Core.Services;
using IslamicPOS.Core.Models;
using QuestPDF.Fluent;
using QuestPDF.Infrastructure;

namespace IslamicPOS.Infrastructure.Services
{
    public class ReceiptService : IReceiptService
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger<ReceiptService> _logger;
        private readonly IIslamicFinanceService _islamicFinanceService;

        public ReceiptService(
            IConfiguration configuration,
            ILogger<ReceiptService> logger,
            IIslamicFinanceService islamicFinanceService)
        {
            _configuration = configuration;
            _logger = logger;
            _islamicFinanceService = islamicFinanceService;
        }

        public async Task<string> GenerateReceipt(Transaction transaction)
        {
            var receipt = new StringBuilder();

            // Add store header
            receipt.AppendLine(_configuration["Store:Name"]);
            receipt.AppendLine(_configuration["Store:Address"]);
            receipt.AppendLine(_configuration["Store:Phone"]);
            receipt.AppendLine(new string('-', 40));

            // Add Halal certification
            receipt.AppendLine("HALAL CERTIFIED");
            receipt.AppendLine($"Cert No: {_configuration["Store:HalalCertificationNumber"]}");
            receipt.AppendLine(new string('-', 40));

            // Transaction details
            receipt.AppendLine($"Date: {transaction.Timestamp:g}");
            receipt.AppendLine($"Transaction #: {transaction.Id}");
            receipt.AppendLine(new string('-', 40));

            // Items
            foreach (var item in transaction.Items)
            {
                receipt.AppendLine($"{item.ProductName}");
                receipt.AppendLine($"  {item.Quantity} x {item.UnitPrice:C} = {item.Subtotal:C}");
            }

            receipt.AppendLine(new string('-', 40));

            // Totals
            receipt.AppendLine($"Subtotal: {transaction.Items.Sum(i => i.Subtotal):C}");
            receipt.AppendLine($"Tax: {transaction.TaxAmount:C}");
            receipt.AppendLine($"Total: {transaction.TotalAmount:C}");

            // Islamic Finance Information
            if (transaction.ZakatAmount > 0)
            {
                receipt.AppendLine(new string('-', 40));
                receipt.AppendLine("Islamic Finance Information");
                receipt.AppendLine($"Zakat Amount: {transaction.ZakatAmount:C}");
                receipt.AppendLine($"Merchant Share: {transaction.MerchantShare:C}");
                receipt.AppendLine($"Partner Share: {transaction.PartnerShare:C}");
            }

            // Payment method
            receipt.AppendLine(new string('-', 40));
            receipt.AppendLine($"Payment Method: {transaction.PaymentMethod}");

            // Footer
            receipt.AppendLine(new string('-', 40));
            receipt.AppendLine("Thank you for your business");
            receipt.AppendLine("May Allah bless your purchase");

            return receipt.ToString();
        }

        public async Task<byte[]> GenerateReceiptPdf(Transaction transaction)
        {
            var document = Document.Create(container =>
            {
                container.Page(page =>
                {
                    page.Size(PageSizes.Roll80);
                    page.Margin(5, Unit.Millimetre);
                    page.Content().Column(column =>
                    {
                        // Header
                        column.Item().Text(_configuration["Store:Name"]).Bold().Center();
                        column.Item().Text(_configuration["Store:Address"]).Center();
                        column.Item().Text(_configuration["Store:Phone"]).Center();

                        // Halal certification
                        column.Item().LineHorizontal(1);
                        column.Item().Text("HALAL CERTIFIED").Bold().Center();
                        column.Item().Text($"Cert No: {_configuration["Store:HalalCertificationNumber"]}").Center();
                        column.Item().LineHorizontal(1);

                        // Transaction details
                        column.Item().Text($"Date: {transaction.Timestamp:g}");
                        column.Item().Text($"Transaction #: {transaction.Id}");
                        column.Item().LineHorizontal(1);

                        // Items
                        foreach (var item in transaction.Items)
                        {
                            column.Item().Text(item.ProductName);
                            column.Item().Text($"  {item.Quantity} x {item.UnitPrice:C} = {item.Subtotal:C}").Right();
                        }

                        // Totals
                        column.Item().LineHorizontal(1);
                        column.Item().Text($"Subtotal: {transaction.Items.Sum(i => i.Subtotal):C}").Right();
                        column.Item().Text($"Tax: {transaction.TaxAmount:C}").Right();
                        column.Item().Text($"Total: {transaction.TotalAmount:C}").Bold().Right();

                        // Islamic Finance Information
                        if (transaction.ZakatAmount > 0)
                        {
                            column.Item().LineHorizontal(1);
                            column.Item().Text("Islamic Finance Information").Bold();
                            column.Item().Text($"Zakat Amount: {transaction.ZakatAmount:C}");
                            column.Item().Text($"Merchant Share: {transaction.MerchantShare:C}");
                            column.Item().Text($"Partner Share: {transaction.PartnerShare:C}");
                        }

                        // Payment method
                        column.Item().LineHorizontal(1);
                        column.Item().Text($"Payment Method: {transaction.PaymentMethod}");

                        // Footer
                        column.Item().LineHorizontal(1);
                        column.Item().Text("Thank you for your business").Center();
                        column.Item().Text("May Allah bless your purchase").Center();
                    });
                });
            });

            return document.GeneratePdf();
        }

        public async Task PrintReceipt(Transaction transaction, string printerName = null)
        {
            try
            {
                var pdfBytes = await GenerateReceiptPdf(transaction);
                var printerSettings = await GetPrinterSettings(printerName);

                using var printDocument = new PrintDocument();
                if (!string.IsNullOrEmpty(printerSettings.PrinterName))
                {
                    printDocument.PrinterSettings.PrinterName = printerSettings.PrinterName;
                }

                // Set up print document properties based on settings
                printDocument.DefaultPageSettings.Copies = (short)printerSettings.Copies;

                // TODO: Implement actual printing using System.Drawing.Common
                // This will need to be tested on the actual deployment environment
                _logger.LogInformation($"Printing receipt for transaction {transaction.Id}");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error printing receipt for transaction {transaction.Id}");
                throw;
            }
        }

        public async Task<List<string>> GetAvailablePrinters()
        {
            return PrinterSettings.InstalledPrinters.Cast<string>().ToList();
        }

        public async Task<PrinterSettings> GetPrinterSettings(string printerName)
        {
            // TODO: Load from configuration or database
            return new PrinterSettings
            {
                PrinterName = printerName,
                IsDefault = string.IsNullOrEmpty(printerName),
                PaperSize = "80mm",
                Copies = 1,
                PrintLogo = true,
                PrintHalalCertification = true,
                HeaderText = _configuration["Store:Name"],
                FooterText = "Thank you for your business\nMay Allah bless your purchase"
            };
        }

        public async Task SavePrinterSettings(PrinterSettings settings)
        {
            // TODO: Save to configuration or database
            _logger.LogInformation($"Saving printer settings for {settings.PrinterName}");
        }
    }
}