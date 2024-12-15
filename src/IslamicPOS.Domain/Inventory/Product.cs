using IslamicPOS.Core.Common;

namespace IslamicPOS.Domain.Inventory;

public class Product : Entity
{
    public string Name { get; private set; }
    public string Code { get; private set; }
    public Money Price { get; private set; }
    public bool IsHalal { get; private set; }
    public string Category { get; private set; }
    public bool IsActive { get; private set; }

    private Product(
        string name,
        string code,
        Money price,
        string category,
        bool isHalal = true)
    {
        Name = name;
        Code = code;
        Price = price;
        Category = category;
        IsHalal = isHalal;
        IsActive = true;
    }

    public static Product Create(
        string name,
        string code,
        Money price,
        string category,
        bool isHalal = true)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Product name is required", nameof(name));

        if (string.IsNullOrWhiteSpace(code))
            throw new ArgumentException("Product code is required", nameof(code));

        return new Product(name, code, price, category, isHalal);
    }

    public void UpdatePrice(Money newPrice)
    {
        if (!IsActive)
            throw new InvalidOperationException("Cannot update price of inactive product");

        Price = newPrice;
    }

    public void UpdateHalalStatus(bool isHalal)
    {
        IsHalal = isHalal;
        UpdatedAt = DateTime.UtcNow;
    }

    public void Deactivate()
    {
        IsActive = false;
        UpdatedAt = DateTime.UtcNow;
    }

    public void Reactivate()
    {
        IsActive = true;
        UpdatedAt = DateTime.UtcNow;
    }
}