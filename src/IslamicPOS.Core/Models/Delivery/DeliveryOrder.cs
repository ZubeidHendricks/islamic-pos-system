namespace IslamicPOS.Core.Models.Delivery;

public class DeliveryOrder : AuditableEntity
{
    public Guid TransactionId { get; set; }
    public Transaction? Transaction { get; set; }
    public Guid DeliveryPointId { get; set; }
    public DeliveryPoint? DeliveryPoint { get; set; }
    public DateTime RequestedDate { get; set; }
    public TimeSpan DeliveryWindow { get; set; }
    public string Status { get; set; } = string.Empty;
    public string? Notes { get; set; }
}
