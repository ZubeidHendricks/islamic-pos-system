using IslamicPOS.Core.Models.Transactions;

namespace IslamicPOS.Core.Models.Inventory
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string SKU { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public int StockLevel { get; set; }
        public int MinimumStockLevel { get; set; }
        public bool IsHalal { get; set; }
        public string HalalCertification { get; set; } = string.Empty;
        public int CategoryId { get; set; }
        public ProductCategory Category { get; set; } = null!;
    }

    public class ProductCategory
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public bool IsActive { get; set; } = true;
        public List<Product> Products { get; set; } = new();
    }

    public class StockMovement
    {
        public int Id { get; set; }
        public DateTime Timestamp { get; set; }
        public MovementType Type { get; set; }
        public int ProductId { get; set; }
        public Product Product { get; set; } = null!;
        public int Quantity { get; set; }
        public string Notes { get; set; } = string.Empty;
    }

    public enum MovementType
    {
        Purchase,
        Sale,
        Return,
        Adjustment,
        Damaged
    }
}