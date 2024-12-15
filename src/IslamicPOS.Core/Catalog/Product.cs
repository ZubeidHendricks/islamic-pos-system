using IslamicPOS.Core.Common;

namespace IslamicPOS.Core.Catalog;

public class Product : Entity
{
    public string Name { get; private set; }
    public string Sku { get; private set; }
    public string Description { get; private set; }
    public ProductCategory Category { get; private set; }
    public Money Price { get; private set; }
    public bool IsActive { get; private set; }
    public bool IsHalalVerified { get; private set; }
    
    private Product(
        string name,
        string sku,
        string description,
        ProductCategory category,
        Money price,
        bool isHalalVerified)
    {
        Name = name;
        Sku = sku;
        Description = description;
        Category = category;
        Price = price;
        IsActive = true;
        IsHalalVerified = isHalalVerified;
    }
    
    public static Product Create(
        string name,
        string sku,
        string description,
        ProductCategory category,
        Money price,
        bool isHalalVerified)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Name is required", nameof(name));
            
        if (string.IsNullOrWhiteSpace(sku))
            throw new ArgumentException("SKU is required", nameof(sku));
            
        return new Product(name, sku, description, category, price, isHalalVerified);
    }
    
    public void UpdatePrice(Money newPrice)
    {
        if (!IsActive)
            throw new InvalidOperationException("Cannot update price of inactive product");
            
        Price = newPrice;
    }
    
    public void Deactivate()
    {
        if (!IsActive)
            throw new InvalidOperationException("Product is already inactive");
            
        IsActive = false;
    }
    
    public void UpdateHalalStatus(bool isHalal)
    {
        IsHalalVerified = isHalal;
    }
}