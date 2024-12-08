using IslamicPOS.Core.Models.Common;
using IslamicPOS.Core.Models.Catalog;

namespace IslamicPOS.Core.Models.Transactions;

public class Transaction : Entity
{
    public string TransactionNumber { get; set; } = string.Empty;
    public decimal TotalAmount { get; set; }
    public string TransactionType { get; set; } = string.Empty;
    public string Status { get; set; } = string.Empty;
    public string UserId { get; set; } = string.Empty;
    public List<TransactionItem> Items { get; set; } = new();
    public string PaymentMethod { get; set; } = string.Empty;
    public DateTime TransactionDate { get; set; }
    public decimal SubTotal { get; set; }
    public decimal Tax { get; set; }
    public decimal Discount { get; set; }
    public string? Notes { get; set; }
}

public class TransactionItem : Entity
{
    public Guid TransactionId { get; set; }
    public Transaction Transaction { get; set; } = null!;
    public Guid ProductId { get; set; }
    public Product Product { get; set; } = null!;
    public int Quantity { get; set; }
    public decimal UnitPrice { get; set; }
    public decimal Discount { get; set; }
    public decimal Total { get; set; }
    public string? Notes { get; set; }
}