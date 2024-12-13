using Microsoft.VisualStudio.TestTools.UnitTesting;
using IslamicPOS.Core.Services;
using IslamicPOS.Infrastructure.Services;

namespace IslamicPOS.Tests.Integration
{
    [TestClass]
    public class PrintSystemTests
    {
        private IReceiptService _receiptService;
        private PrinterConfigurationService _printerConfig;
        private PrinterIntegrationService _printerIntegration;

        [TestInitialize]
        public async Task Setup()
        {
            var configuration = new TestConfiguration();
            var context = await TestDbContextFactory.CreateAsync();
            var logger = new TestLogger<PrinterIntegrationService>();

            _printerIntegration = new PrinterIntegrationService(logger);
            _printerConfig = new PrinterConfigurationService(context);
            _receiptService = new ReceiptService(configuration, logger, null);

            // Set up test data
            await context.PrinterConfigurations.AddAsync(new PrinterConfigurationEntity
            {
                PrinterName = "TestPrinter",
                IsDefault = true,
                PaperSize = "80mm"
            });
            await context.SaveChangesAsync();
        }

        [TestMethod]
        public async Task PrintReceipt_WithValidTransaction_PrintsSuccessfully()
        {
            // Arrange
            var transaction = CreateTestTransaction();
            var config = await _printerConfig.GetPrinterSettings("TestPrinter");

            // Act & Assert
            await _receiptService.PrintReceipt(transaction, config.PrinterName);
        }

        [TestMethod]
        public async Task PrintReceipt_WithIslamicContent_ContainsRequiredElements()
        {
            // Arrange
            var transaction = CreateTestTransaction(includeZakat: true);

            // Act
            var receipt = await _receiptService.GenerateReceipt(transaction);

            // Assert
            Assert.IsTrue(receipt.Contains("Halal Certified"));
            Assert.IsTrue(receipt.Contains("Zakat Amount:"));
            Assert.IsTrue(receipt.Contains("May Allah bless your purchase"));
        }

        [TestMethod]
        public async Task PrinterStatus_WhenPrinterOffline_ReportsCorrectly()
        {
            // Arrange
            var printerName = "OfflinePrinter";

            // Act
            var status = await _printerIntegration.GetPrinterStatus(printerName);

            // Assert
            Assert.IsFalse(status.IsOnline);
            Assert.IsNotNull(status.Status);
        }

        [TestMethod]
        public async Task MultiPrinterSetup_WithDefaultPrinter_UsesCorrectPrinter()
        {
            // Arrange
            await _printerConfig.SavePrinterSettings(new PrinterSettings
            {
                PrinterName = "Printer1",
                IsDefault = true
            });
            await _printerConfig.SavePrinterSettings(new PrinterSettings
            {
                PrinterName = "Printer2",
                IsDefault = false
            });

            // Act
            var defaultSettings = await _printerConfig.GetPrinterSettings();

            // Assert
            Assert.AreEqual("Printer1", defaultSettings.PrinterName);
        }

        private Transaction CreateTestTransaction(bool includeZakat = false)
        {
            decimal amount = includeZakat ? 6000m : 100m; // Above Nisab threshold if including Zakat
            return new Transaction
            {
                Id = Guid.NewGuid(),
                Timestamp = DateTime.UtcNow,
                Items = new List<TransactionItem>
                {
                    new()
                    {
                        ProductName = "Test Product",
                        Quantity = 1,
                        UnitPrice = amount,
                        Subtotal = amount
                    }
                },
                TotalAmount = amount,
                PaymentMethod = PaymentMethod.Cash,
                ZakatAmount = includeZakat ? amount * 0.025m : 0
            };
        }
    }
}