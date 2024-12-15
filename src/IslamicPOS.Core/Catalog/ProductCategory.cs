using IslamicPOS.Core.Common;

namespace IslamicPOS.Core.Catalog;

public class ProductCategory : Entity
{
    private readonly List<Product> _products = new();
    
    public string Name { get; private set; }
    public string Description { get; private set; }
    public bool IsActive { get; private set; }
    public IReadOnlyCollection<Product> Products => _products.AsReadOnly();
    
    private ProductCategory(string name, string description)
    {
        Name = name;
        Description = description;
        IsActive = true;
    }
    
    public static ProductCategory Create(string name, string description)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Name is required", nameof(name));
            
        return new ProductCategory(name, description);
    }
}