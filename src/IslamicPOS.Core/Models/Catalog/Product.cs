using IslamicPOS.Core.Models.Common;

namespace IslamicPOS.Core.Models.Catalog;

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
    public string? Brand { get; set; }
    public string? Supplier { get; set; }
    public decimal? MinimumStockLevel { get; set; }
    public decimal? MaximumStockLevel { get; set; }
    public decimal? ReorderPoint { get; set; }
}