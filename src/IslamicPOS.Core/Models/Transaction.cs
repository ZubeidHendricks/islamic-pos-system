using IslamicPOS.Core.Models.Inventory;

namespace IslamicPOS.Core.Models;

public class Transaction : AuditableEntity
{
    public string TransactionType { get; set; } = string.Empty;
    public string Status { get; set; } = string.Empty;
    public decimal TotalAmount { get; set; }
    public List<TransactionItem> Items { get; set; } = new();
    public string? Notes { get; set; }
}

public class TransactionItem
{
    public Guid Id { get; set; }
    public Guid TransactionId { get; set; }
    public Transaction? Transaction { get; set; }
    public Guid ProductId { get; set; }
    public Product? Product { get; set; }
    public int Quantity { get; set; }
    public decimal UnitPrice { get; set; }
    public decimal Discount { get; set; }
    public decimal Total { get; set; }
    public string? Notes { get; set; }
}
