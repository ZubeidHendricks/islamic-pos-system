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
}