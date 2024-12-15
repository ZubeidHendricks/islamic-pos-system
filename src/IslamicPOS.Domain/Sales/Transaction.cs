using IslamicPOS.Domain.Common;

namespace IslamicPOS.Domain.Sales;

public class Transaction : EntityBase
{
    public DateTime TransactionDate { get; private set; }
    public string CustomerId { get; private set; } = string.Empty;
    public List<TransactionItem> Items { get; private set; } = new();
    public Money Total { get; private set; } = Money.Zero();
    public string PaymentMethod { get; private set; } = string.Empty;
    public bool IsCompliant { get; private set; }
    public string Status { get; private set; } = string.Empty;

    private Transaction() { } // For EF Core

    public Transaction(
        string customerId,
        List<TransactionItem> items,
        string paymentMethod,
        bool isCompliant)
    {
        CustomerId = customerId;
        Items = items;
        PaymentMethod = paymentMethod;
        IsCompliant = isCompliant;
        TransactionDate = DateTime.UtcNow;
        Status = "Created";
        CalculateTotal();
    }

    private void CalculateTotal()
    {
        Total = Money.Zero(Items.FirstOrDefault()?.UnitPrice.Currency ?? "USD");
        foreach (var item in Items)
        {
            Total += item.UnitPrice * item.Quantity;
        }
    }
}

public class TransactionItem : EntityBase
{
    public string ProductId { get; private set; } = string.Empty;
    public int Quantity { get; private set; }
    public Money UnitPrice { get; private set; } = Money.Zero();
    public string Notes { get; private set; } = string.Empty;

    private TransactionItem() { } // For EF Core

    public TransactionItem(string productId, int quantity, Money unitPrice, string notes = "")
    {
        ProductId = productId;
        Quantity = quantity;
        UnitPrice = unitPrice;
        Notes = notes;
    }
}
