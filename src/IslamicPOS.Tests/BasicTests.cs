using Microsoft.VisualStudio.TestTools.UnitTesting;
using IslamicPOS.Core.Models;

namespace IslamicPOS.Tests
{
    [TestClass]
    public class BasicTests
    {
        [TestMethod]
        public void TransactionItem_CalculatesSubtotal()
        {
            // Arrange
            var item = new TransactionItem
            {
                ProductName = "Test Product",
                UnitPrice = 10.0m,
                Quantity = 2
            };

            // Act
            var subtotal = item.Subtotal;

            // Assert
            Assert.AreEqual(20.0m, subtotal);
        }

        [TestMethod]
        public void PrinterSettings_HasDefaultValues()
        {
            // Arrange & Act
            var settings = new PrinterSettings();

            // Assert
            Assert.AreEqual("80mm", settings.PaperSize);
            Assert.AreEqual(1, settings.Copies);
            Assert.IsTrue(settings.PrintLogo);
            Assert.IsTrue(settings.PrintHalalCertification);
        }
    }
}