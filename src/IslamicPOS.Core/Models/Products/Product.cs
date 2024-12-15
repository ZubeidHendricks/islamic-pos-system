using IslamicPOS.Core.Common;
using IslamicPOS.Core.Models.Common;

namespace IslamicPOS.Core.Models.Products;

public class Product : Entity
{
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public string SKU { get; set; } = string.Empty;
    public int StockQuantity { get; set; }
    public string Category { get; set; } = string.Empty;
    public bool IsHalal { get; set; }
    public string HalalCertification { get; set; } = string.Empty;
}
