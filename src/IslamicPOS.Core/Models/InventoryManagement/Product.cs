using IslamicPOS.Core.Models.Base;
using IslamicPOS.Core.Models.Common;

namespace IslamicPOS.Core.Models.InventoryManagement
{
    public class ProductItem : EntityBase
    {
        public string Name { get; private set; }
        public string Description { get; private set; }
        public string SKU { get; private set; }
        public string Barcode { get; private set; }
        public Money Price { get; private set; }
        public int StockQuantity { get; private set; }
        public bool IsHalalVerified { get; private set; }
        public string HalalCertificationNumber { get; private set; }
        public ProductCategory Category { get; private set; }
        public int CategoryId { get; private set; }

        private ProductItem() {} // For EF Core

        public ProductItem(string name, string description, string sku, Money price, ProductCategory category)
        {
            Name = name;
            Description = description;
            SKU = sku;
            Price = price;
            Category = category;
            CategoryId = category.Id;
        }

        public void UpdatePrice(Money newPrice)
        {
            Price = newPrice;
            UpdatedAt = DateTime.UtcNow;
        }
    }
}