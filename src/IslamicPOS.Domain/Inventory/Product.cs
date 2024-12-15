using IslamicPOS.Domain.Common;

namespace IslamicPOS.Domain.Inventory;

public class Product : EntityBase
{
    public new Guid Id { get; private set; }
    public string Name { get; private set; } = string.Empty;
    public string Description { get; private set; } = string.Empty;
    public string SKU { get; private set; } = string.Empty;
    public Money Price { get; private set; } = Money.Zero();
    public int StockQuantity { get; private set; }
    public bool IsHalal { get; private set; }
    public string HalalCertification { get; private set; } = string.Empty;

    protected Product() { } // For EF Core

    public Product(
        string name,
        string description,
        string sku,
        Money price,
        bool isHalal = true,
        string halalCertification = "")
    {
        Id = Guid.NewGuid();
        Name = name;
        Description = description;
        SKU = sku;
        Price = price;
        IsHalal = isHalal;
        HalalCertification = halalCertification;
        StockQuantity = 0;
    }

    public void UpdateStock(int quantity)
    {
        StockQuantity += quantity;
    }

    public void UpdatePrice(Money newPrice)
    {
        if (newPrice.Amount < 0)
            throw new ArgumentException("Price cannot be negative", nameof(newPrice));

        Price = newPrice;
    }
}
