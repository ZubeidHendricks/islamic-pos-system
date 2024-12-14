using IslamicPOS.Core.Models.Base;

namespace IslamicPOS.Core.Models.Product
{
    public class ProductCategory : EntityBase
    {
        public string Name { get; private set; }
        public string Description { get; private set; }
        public bool RequiresHalalCertification { get; private set; }
        public ICollection<Product> Products { get; private set; }

        private ProductCategory() {} // For EF Core

        public ProductCategory(string name, string description, bool requiresHalalCertification = true)
        {
            Name = name;
            Description = description;
            RequiresHalalCertification = requiresHalalCertification;
            Products = new List<Product>();
        }
    }
}