using IslamicPOS.Domain.Common;

namespace IslamicPOS.Domain.Sales;

public class Sale : EntityBase
{
    public string ReferenceNumber { get; private set; } = string.Empty;
    public DateTime SaleDate { get; private set; }
    public Money TotalAmount { get; private set; } = Money.Zero();
    public List<SaleItem> Items { get; private set; } = new();
    public string CustomerId { get; private set; } = string.Empty;
    public string Status { get; private set; } = string.Empty;
    
    private Sale() { } // For EF Core

    public Sale(string customerId, List<SaleItem> items)
    {
        CustomerId = customerId;
        Items = items;
        SaleDate = DateTime.UtcNow;
        ReferenceNumber = GenerateReferenceNumber();
        Status = "Created";
        CalculateTotal();
    }

    private void CalculateTotal()
    {
        if (!Items.Any())
            return;

        var total = Money.Zero(Items.First().UnitPrice.Currency);
        foreach (var item in Items)
        {
            // Use Amount property for addition
            total = new Money(total.Amount + (item.UnitPrice.Amount * item.Quantity), item.UnitPrice.Currency);
        }
        TotalAmount = total;
    }

    private string GenerateReferenceNumber()
    {
        return $"SALE-{DateTime.UtcNow:yyyyMMdd}-{Guid.NewGuid().ToString().Substring(0, 8).ToUpper()}";
    }
}

public class SaleItem : EntityBase
{
    public string ProductId { get; private set; } = string.Empty;
    public int Quantity { get; private set; }
    public Money UnitPrice { get; private set; } = Money.Zero();
    public string Notes { get; private set; } = string.Empty;

    public SaleItem(string productId, int quantity, Money unitPrice, string notes = "")
    {
        ProductId = productId;
        Quantity = quantity;
        UnitPrice = unitPrice;
        Notes = notes;
    }

    private SaleItem() { } // For EF Core
}