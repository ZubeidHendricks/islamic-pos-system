using IslamicPOS.Domain.Common;

namespace IslamicPOS.Domain.Inventory;

public class Product : Entity
{
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public string SKU { get; set; } = string.Empty;
    public int StockQuantity { get; set; } = 0;
    public string Category { get; set; } = string.Empty;
    public bool IsHalal { get; set; }
    public string HalalCertification { get; set; } = string.Empty;

    private Product() {} // For EF Core

    public Product(
        string name, 
        string description, 
        string sku, 
        decimal price, 
        bool isHalal, 
        string halalCertification,
        string category = "", 
        int stockQuantity = 0)
    {
        Name = name;
        Description = description;
        SKU = sku;
        Price = price;
        IsHalal = isHalal;
        HalalCertification = halalCertification;
        Category = category;
        StockQuantity = stockQuantity;
    }
}