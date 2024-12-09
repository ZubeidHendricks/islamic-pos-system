using IslamicPOS.Domain.Common;

namespace IslamicPOS.Domain.Inventory
{
    public class Product : AuditableEntity
    {
        public string Name { get; private set; } = string.Empty;
        public string SKU { get; private set; } = string.Empty;
        public string Description { get; private set; } = string.Empty;
        public Money Price { get; private set; }
        public int StockLevel { get; private set; }
        public int MinimumStockLevel { get; private set; }
        public bool IsHalal { get; private set; }
        public string HalalCertification { get; private set; } = string.Empty;
        public int CategoryId { get; private set; }
        public ProductCategory Category { get; private set; } = null!;

        private Product() { } // For EF Core

        private Product(string name, string sku, string description, Money price, 
            int minimumStockLevel, bool isHalal, string halalCertification, ProductCategory category)
        {
            Name = name;
            SKU = sku;
            Description = description;
            Price = price;
            MinimumStockLevel = minimumStockLevel;
            IsHalal = isHalal;
            HalalCertification = halalCertification;
            Category = category;
            CategoryId = category.Id;
            StockLevel = 0;
        }

        public static Product Create(string name, string sku, string description, Money price,
            int minimumStockLevel, bool isHalal, string halalCertification, ProductCategory category)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Product name cannot be empty", nameof(name));

            if (string.IsNullOrWhiteSpace(sku))
                throw new ArgumentException("SKU cannot be empty", nameof(sku));

            if (isHalal && string.IsNullOrWhiteSpace(halalCertification))
                throw new ArgumentException("Halal certification is required for halal products", nameof(halalCertification));

            return new Product(name, sku, description, price, minimumStockLevel, isHalal, halalCertification, category);
        }

        public void UpdateStock(int quantity)
        {
            if (StockLevel + quantity < 0)
                throw new InvalidOperationException("Cannot reduce stock below zero");

            StockLevel += quantity;
        }

        public void UpdatePrice(Money newPrice)
        {
            if (newPrice.Amount <= 0)
                throw new ArgumentException("Price must be greater than zero", nameof(newPrice));

            Price = newPrice;
        }
    }
}