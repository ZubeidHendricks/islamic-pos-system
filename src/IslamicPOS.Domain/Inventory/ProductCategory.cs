using IslamicPOS.Domain.Common;

namespace IslamicPOS.Domain.Inventory;

public class ProductCategory : EntityBase
{
    public string Name { get; private set; } = string.Empty;
    public string Description { get; private set; } = string.Empty;
    public bool IsHalalCategory { get; private set; }
    public string HalalCertification { get; private set; } = string.Empty;
    public List<Product> Products { get; private set; } = new();

    public ProductCategory(string name, string description, bool isHalalCategory, string halalCertification = "")
    {
        Name = name;
        Description = description;
        IsHalalCategory = isHalalCategory;
        HalalCertification = halalCertification;
    }

    // Required by EF Core
    protected ProductCategory() { }
}
