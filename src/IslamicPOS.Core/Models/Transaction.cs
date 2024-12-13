namespace IslamicPOS.Core.Models
{
    public class Transaction
    {
        public Guid Id { get; set; }
        public DateTime Timestamp { get; set; }
        public List<TransactionItem> Items { get; set; } = new();
        public decimal TotalAmount { get; set; }
        public decimal ZakatAmount { get; set; }
        public decimal MerchantShare { get; set; }
        public decimal PartnerShare { get; set; }
        public PaymentMethod PaymentMethod { get; set; }
        public string ComplianceNotice { get; set; }
    }

    public class TransactionItem
    {
        public string ProductId { get; set; }
        public string ProductName { get; set; }
        public decimal UnitPrice { get; set; }
        public int Quantity { get; set; }
        public decimal Subtotal => UnitPrice * Quantity;
    }

    public enum PaymentMethod
    {
        Cash,
        BankTransfer,
        IslamicCredit,
        IslamicDebit,
        DigitalWallet
    }
}