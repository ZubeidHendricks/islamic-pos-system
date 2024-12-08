namespace IslamicPOS.Core.Models.Delivery;

public class DeliveryOrder
{
    public int Id { get; set; }
    public Guid TransactionId { get; set; }
    public Transaction? Transaction { get; set; }
    public int DeliveryPointId { get; set; }
    public DeliveryPoint? DeliveryPoint { get; set; }
    public DateTime RequestedDeliveryDate { get; set; }
    public TimeSpan? DeliveryWindow { get; set; }
    public string Status { get; set; } = string.Empty;
    public Vehicle? Vehicle { get; set; }
}
