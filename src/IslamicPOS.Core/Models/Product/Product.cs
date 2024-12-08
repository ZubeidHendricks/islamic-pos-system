using IslamicPOS.Core.Models.Common;

namespace IslamicPOS.Core.Models.Product;

public class Product : Entity
{
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string SKU { get; set; } = string.Empty;
    public string Barcode { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public int StockQuantity { get; set; }
    public bool IsActive { get; set; } = true;
    public string? Category { get; set; }
    public decimal? Weight { get; set; }
    public string? Unit { get; set; }
}