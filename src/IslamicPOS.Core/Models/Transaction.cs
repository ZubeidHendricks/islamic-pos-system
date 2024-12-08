using System;
using System.Collections.Generic;
using IslamicPOS.Core.Models.Common;

namespace IslamicPOS.Core.Models;

public class Transaction : Entity
{
    public string TransactionNumber { get; set; } = string.Empty;
    public decimal TotalAmount { get; set; }
    public string TransactionType { get; set; } = string.Empty;
    public string Status { get; set; } = string.Empty;
    public string UserId { get; set; } = string.Empty;
    public List<TransactionItem> Items { get; set; } = new();
    public string PaymentMethod { get; set; } = string.Empty;
    public string? Notes { get; set; }
}

public class TransactionItem
{
    public Guid Id { get; set; }
    public Guid TransactionId { get; set; }
    public Transaction Transaction { get; set; } = null!;
    public Guid ProductId { get; set; }
    public int Quantity { get; set; }
    public decimal UnitPrice { get; set; }
    public decimal Discount { get; set; }
    public decimal Total { get; set; }
    public string? Notes { get; set; }
}