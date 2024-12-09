using IslamicPOS.Domain.Common;
using IslamicPOS.Domain.Inventory;
using IslamicPOS.Domain.ValueObjects;

namespace IslamicPOS.Domain.Sales
{
    public class Sale : AuditableEntity
    {
        public string InvoiceNumber { get; private set; } = string.Empty;
        public DateTime SaleDate { get; private set; }
        public Money TotalAmount { get; private set; }
        public PaymentMethod PaymentMethod { get; private set; }
        public PaymentStatus PaymentStatus { get; private set; }
        public string CustomerName { get; private set; } = string.Empty;
        public string CustomerPhone { get; private set; } = string.Empty;

        private readonly List<SaleItem> _items = new();
        public IReadOnlyList<SaleItem> Items => _items.AsReadOnly();

        private Sale() { } // For EF Core

        private Sale(string invoiceNumber, string customerName, string customerPhone, PaymentMethod paymentMethod)
        {
            InvoiceNumber = invoiceNumber;
            CustomerName = customerName;
            CustomerPhone = customerPhone;
            PaymentMethod = paymentMethod;
            SaleDate = DateTime.UtcNow;
            PaymentStatus = PaymentStatus.Pending;
            TotalAmount = Money.Zero("USD"); // Default currency, should be configurable
        }

        public static Sale Create(string invoiceNumber, string customerName, string customerPhone, PaymentMethod paymentMethod)
        {
            return new Sale(invoiceNumber, customerName, customerPhone, paymentMethod);
        }

        public void AddItem(Product product, int quantity, Money unitPrice)
        {
            if (quantity <= 0)
                throw new ArgumentException("Quantity must be greater than zero", nameof(quantity));

            if (PaymentStatus != PaymentStatus.Pending)
                throw new InvalidOperationException("Cannot modify a completed sale");

            var item = new SaleItem(product.Id, product.Name, quantity, unitPrice);
            _items.Add(item);

            RecalculateTotal();
        }

        public void RemoveItem(int productId)
        {
            if (PaymentStatus != PaymentStatus.Pending)
                throw new InvalidOperationException("Cannot modify a completed sale");

            var item = _items.FirstOrDefault(i => i.ProductId == productId);
            if (item != null)
            {
                _items.Remove(item);
                RecalculateTotal();
            }
        }

        public void CompletePayment()
        {
            if (!_items.Any())
                throw new InvalidOperationException("Cannot complete a sale with no items");

            if (PaymentStatus != PaymentStatus.Pending)
                throw new InvalidOperationException("Payment has already been processed");

            PaymentStatus = PaymentStatus.Completed;
        }

        private void RecalculateTotal()
        {
            if (!_items.Any())
            {
                TotalAmount = Money.Zero("USD");
                return;
            }

            var currency = _items.First().UnitPrice.Currency;
            TotalAmount = _items.Aggregate(Money.Zero(currency),
                (total, item) => total.Add(item.UnitPrice.Multiply(item.Quantity)));
        }
    }

    public enum PaymentMethod
    {
        Cash,
        Card,
        BankTransfer,
        IslamicFinancing
    }

    public enum PaymentStatus
    {
        Pending,
        Completed,
        Cancelled
    }

    public class SaleItem : Entity
    {
        public int ProductId { get; private set; }
        public string ProductName { get; private set; }
        public int Quantity { get; private set; }
        public Money UnitPrice { get; private set; }

        private SaleItem() { } // For EF Core

        public SaleItem(int productId, string productName, int quantity, Money unitPrice)
        {
            ProductId = productId;
            ProductName = productName;
            Quantity = quantity;
            UnitPrice = unitPrice;
        }
    }
}