using IslamicPOS.Core.Common;

namespace IslamicPOS.Core.Models.Products;

public class Product : Entity
{
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string SKU { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public decimal Cost { get; set; }
    public int StockQuantity { get; set; }
    public bool IsHalal { get; set; }
    public string HalalCertification { get; set; } = string.Empty;
    public string Category { get; set; } = string.Empty;
}