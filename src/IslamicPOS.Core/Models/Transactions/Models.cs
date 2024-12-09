using IslamicPOS.Core.Models.Inventory;

namespace IslamicPOS.Core.Models.Transactions
{
    public class Transaction
    {
        public int Id { get; set; }
        public string TransactionNumber { get; set; } = string.Empty;
        public DateTime TransactionDate { get; set; }
        public decimal TotalAmount { get; set; }
        public TransactionType Type { get; set; }
        public PaymentMethod PaymentMethod { get; set; }
        public string Notes { get; set; } = string.Empty;
        public List<TransactionItem> Items { get; set; } = new();
    }

    public class TransactionItem
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public Product Product { get; set; } = null!;
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal Subtotal { get; set; }
    }

    public enum TransactionType
    {
        Sale,
        Purchase,
        Return,
        Adjustment
    }

    public enum PaymentMethod
    {
        Cash,
        Card,
        BankTransfer,
        IslamicFinancing
    }
}