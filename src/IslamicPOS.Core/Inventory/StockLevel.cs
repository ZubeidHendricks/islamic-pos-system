using IslamicPOS.Core.Catalog;
using IslamicPOS.Core.Common;

namespace IslamicPOS.Core.Inventory;

public class StockLevel : Entity
{
    public Product Product { get; private set; }
    public int Quantity { get; private set; }
    public int MinimumLevel { get; private set; }
    public int MaximumLevel { get; private set; }
    public string Location { get; private set; }
    
    private StockLevel(
        Product product,
        int quantity,
        int minimumLevel,
        int maximumLevel,
        string location)
    {
        Product = product;
        Quantity = quantity;
        MinimumLevel = minimumLevel;
        MaximumLevel = maximumLevel;
        Location = location;
    }
    
    public static StockLevel Create(
        Product product,
        int quantity,
        int minimumLevel,
        int maximumLevel,
        string location)
    {
        if (minimumLevel < 0)
            throw new ArgumentException("Minimum level cannot be negative");
            
        if (maximumLevel <= minimumLevel)
            throw new ArgumentException("Maximum level must be greater than minimum level");
            
        if (quantity < 0)
            throw new ArgumentException("Quantity cannot be negative");
            
        return new StockLevel(product, quantity, minimumLevel, maximumLevel, location);
    }
    
    public void AdjustStock(int adjustment)
    {
        var newQuantity = Quantity + adjustment;
        if (newQuantity < 0)
            throw new InvalidOperationException("Stock level cannot go below zero");
            
        Quantity = newQuantity;
    }
    
    public bool IsLowStock() => Quantity <= MinimumLevel;
    
    public bool IsOverStock() => Quantity >= MaximumLevel;
}