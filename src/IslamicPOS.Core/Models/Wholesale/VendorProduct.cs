namespace IslamicPOS.Core.Models.Wholesale;

public class VendorProduct
{
    public Guid Id { get; set; }
    public int VendorId { get; set; }
    public string SKU { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public decimal WholesalePrice { get; set; }
    public decimal RetailPrice { get; set; }
    public int MinimumOrderQuantity { get; set; }
    public int AvailableStock { get; set; }
    public string Category { get; set; } = string.Empty;
    public bool IsHalal { get; set; }
    public string HalalCertification { get; set; } = string.Empty;
    public string ManufacturingLocation { get; set; } = string.Empty;
    public DateTime ExpiryDate { get; set; }
    public int LeadTimeInDays { get; set; }
    public bool AutoApproveOrders { get; set; }
    public decimal BulkDiscountThreshold { get; set; }
    public decimal BulkDiscountPercentage { get; set; }
    public DateTime LastUpdated { get; set; }
    
    // Calculated fields
    public decimal ProfitMargin => RetailPrice > 0 ? 
        ((RetailPrice - WholesalePrice) / RetailPrice) * 100 : 0;
    
    public bool IsLowStock => AvailableStock <= MinimumOrderQuantity * 2;
    
    public bool IsExpiringSoon => ExpiryDate <= DateTime.Now.AddMonths(3);
}