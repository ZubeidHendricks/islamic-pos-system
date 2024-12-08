namespace IslamicPOS.Core.Models.InventoryManagement;

public class StockMovement
{
    public int Id { get; set; }
    public Guid ProductId { get; set; }
    public int Quantity { get; set; }
    public string Type { get; set; } = string.Empty; // Purchase, Sale, Adjustment, Return
    public string Reference { get; set; } = string.Empty; // Reference to related transaction/document
    public string Notes { get; set; } = string.Empty;
    public DateTime Timestamp { get; set; } = DateTime.UtcNow;
    public string CreatedBy { get; set; } = string.Empty;

    // Navigation property
    public Product Product { get; set; } = null!;
}