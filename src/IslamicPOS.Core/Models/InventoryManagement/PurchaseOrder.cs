namespace IslamicPOS.Core.Models.InventoryManagement;

public class PurchaseOrder
{
    public int Id { get; set; }
    public string OrderNumber { get; set; } = string.Empty;
    public int SupplierId { get; set; }
    public DateTime OrderDate { get; set; }
    public DateTime? ExpectedDeliveryDate { get; set; }
    public string Status { get; set; } = string.Empty; // Draft, Confirmed, Received, Cancelled
    public decimal TotalAmount { get; set; }
    public string Notes { get; set; } = string.Empty;
    public string CreatedBy { get; set; } = string.Empty;

    // Navigation properties
    public Supplier Supplier { get; set; } = null!;
    public ICollection<PurchaseOrderItem> Items { get; set; } = new List<PurchaseOrderItem>();
}