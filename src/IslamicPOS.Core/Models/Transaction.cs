namespace IslamicPOS.Core.Models;

public class Transaction
{
    public Guid Id { get; set; }
    public DateTime TransactionDate { get; set; }
    public decimal TotalAmount { get; set; }
    public string TransactionType { get; set; } = string.Empty;
    public string Status { get; set; } = string.Empty;
    public List<TransactionItem> Items { get; set; } = new List<TransactionItem>();
}

public class TransactionItem
{
    public int Id { get; set; }
    public Guid TransactionId { get; set; }
    public Transaction? Transaction { get; set; }
    public int ProductId { get; set; }
    public Product? Product { get; set; }
    public int Quantity { get; set; }
    public decimal UnitPrice { get; set; }
    public decimal Subtotal { get; set; }
    public decimal Discount { get; set; }
}
