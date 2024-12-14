using IslamicPOS.Core.Models.Base;
using IslamicPOS.Core.Models.Common;

namespace IslamicPOS.Core.Models.Product
{
    public class Product : AuditableEntity
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
        public DateTime? ExpiryDate { get; private set; }
        public string Supplier { get; private set; }

        private Product() {} // For EF Core

        public Product(string name, string description, string sku, Money price, ProductCategory category)
        {
            Name = name;
            Description = description;
            SKU = sku;
            Price = price;
            Category = category;
            CategoryId = category.Id;
            StockQuantity = 0;
            IsHalalVerified = false;
        }

        public void UpdateStock(int quantity)
        {
            if (StockQuantity + quantity < 0)
                throw new InvalidOperationException("Insufficient stock");

            StockQuantity += quantity;
            UpdatedAt = DateTime.UtcNow;
        }

        public void VerifyHalal(string certificationNumber)
        {
            IsHalalVerified = true;
            HalalCertificationNumber = certificationNumber;
            UpdatedAt = DateTime.UtcNow;
        }

        public void UpdatePrice(Money newPrice)
        {
            Price = newPrice;
            UpdatedAt = DateTime.UtcNow;
        }
    }
}