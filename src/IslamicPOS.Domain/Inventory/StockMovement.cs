using IslamicPOS.Domain.Common;

namespace IslamicPOS.Domain.Inventory;

public class StockMovement : Entity
{
    public int ProductId { get; set; }
    public Product Product { get; set; } = null!;
    public int Quantity { get; set; }
    public DateTime MovementDate { get; set; }
    public string MovementType { get; set; } = string.Empty; // e.g. 'Inbound', 'Outbound'
}