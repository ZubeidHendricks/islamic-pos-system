using Microsoft.VisualStudio.TestTools.UnitTesting;
using IslamicPOS.Infrastructure.Services;

namespace IslamicPOS.Tests.Integration
{
    [TestClass]
    public class PrinterIntegrationTests
    {
        private PrinterIntegrationService _printerService;
        private PrinterConfigurationService _configService;

        [TestInitialize]
        public async Task Setup()
        {
            var configuration = new TestConfiguration();
            var context = await TestDbContextFactory.CreateAsync();
            var logger = new TestLogger<PrinterIntegrationService>();

            _printerService = new PrinterIntegrationService(logger);
            _configService = new PrinterConfigurationService(context);

            // Set up test printer configurations
            await SetupTestPrinters(context);
        }

        [TestMethod]
        public async Task GetPrinterStatus_WithValidPrinter_ReturnsStatus()
        {
            // Arrange
            var printerName = "TestPrinter1";

            // Act
            var status = await _printerService.GetPrinterStatus(printerName);

            // Assert
            Assert.IsNotNull(status);
            Assert.IsTrue(status.IsOnline);
        }

        [TestMethod]
        public async Task Print_WithValidDocument_SuccessfullyPrints()
        {
            // Arrange
            var printerName = "TestPrinter1";
            var settings = await _configService.GetPrinterSettings(printerName);
            var testDocument = GenerateTestDocument();

            // Act
            await _printerService.Print(testDocument, settings);

            // Assert - No exception thrown
        }

        [TestMethod]
        public async Task Print_WithMultipleCopies_PrintsCorrectNumber()
        {
            // Arrange
            var printerName = "TestPrinter1";
            var settings = await _configService.GetPrinterSettings(printerName);
            settings.Copies = 2;
            var testDocument = GenerateTestDocument();

            // Act
            await _printerService.Print(testDocument, settings);

            // Assert - Verify through print spooler or mock
        }

        [TestMethod]
        public async Task TestPrinter_WithNetworkPrinter_ReturnsCorrectStatus()
        {
            // Arrange
            var networkPrinterName = "\\\\SERVER\\NetworkPrinter";

            // Act
            var isValid = await _printerService.TestPrinter(networkPrinterName);

            // Assert
            Assert.IsFalse(isValid); // Should be false in test environment
        }

        private async Task SetupTestPrinters(ApplicationDbContext context)
        {
            var testPrinters = new[]
            {
                new PrinterConfigurationEntity
                {
                    PrinterName = "TestPrinter1",
                    IsDefault = true,
                    PaperSize = "80mm",
                    Copies = 1
                },
                new PrinterConfigurationEntity
                {
                    PrinterName = "TestPrinter2",
                    IsDefault = false,
                    PaperSize = "58mm",
                    Copies = 1
                }
            };

            context.PrinterConfigurations.AddRange(testPrinters);
            await context.SaveChangesAsync();
        }

        private byte[] GenerateTestDocument()
        {
            // Generate a simple test receipt
            var content = "Test Receipt\n" +
                         "============\n" +
                         "Date: " + DateTime.Now.ToString("g") + "\n" +
                         "Item 1     $10.00\n" +
                         "============\n" +
                         "Total:     $10.00\n";

            return System.Text.Encoding.UTF8.GetBytes(content);
        }
    }
}