using IslamicPOS.Core.Catalog;
using IslamicPOS.Core.Common;

namespace IslamicPOS.Core.Inventory;

public class StockMovement : Entity
{
    public Product Product { get; private set; }
    public int Quantity { get; private set; }
    public MovementType Type { get; private set; }
    public string FromLocation { get; private set; }
    public string ToLocation { get; private set; }
    public string? Reference { get; private set; }
    public DateTime MovementDate { get; private set; }
    
    private StockMovement(
        Product product,
        int quantity,
        MovementType type,
        string fromLocation,
        string toLocation,
        string? reference)
    {
        Product = product;
        Quantity = quantity;
        Type = type;
        FromLocation = fromLocation;
        ToLocation = toLocation;
        Reference = reference;
        MovementDate = DateTime.UtcNow;
    }
    
    public static StockMovement Create(
        Product product,
        int quantity,
        MovementType type,
        string fromLocation,
        string toLocation,
        string? reference = null)
    {
        if (quantity <= 0)
            throw new ArgumentException("Quantity must be greater than zero");
            
        if (string.IsNullOrWhiteSpace(fromLocation))
            throw new ArgumentException("From location is required");
            
        if (string.IsNullOrWhiteSpace(toLocation))
            throw new ArgumentException("To location is required");
            
        return new StockMovement(product, quantity, type, fromLocation, toLocation, reference);
    }
}