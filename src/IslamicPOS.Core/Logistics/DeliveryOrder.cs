using IslamicPOS.Core.Common;
using IslamicPOS.Core.Sales;

namespace IslamicPOS.Core.Logistics;

public class DeliveryOrder : Entity
{
    public Transaction Transaction { get; private set; }
    public Address DeliveryAddress { get; private set; }
    public DeliveryStatus Status { get; private set; }
    public DateTime? ScheduledDate { get; private set; }
    public DateTime? DeliveredDate { get; private set; }
    public string? Notes { get; private set; }
    
    private DeliveryOrder(
        Transaction transaction,
        Address deliveryAddress,
        DateTime? scheduledDate = null,
        string? notes = null)
    {
        Transaction = transaction;
        DeliveryAddress = deliveryAddress;
        Status = DeliveryStatus.Created;
        ScheduledDate = scheduledDate;
        Notes = notes;
    }
    
    public static DeliveryOrder Create(
        Transaction transaction,
        Address deliveryAddress,
        DateTime? scheduledDate = null,
        string? notes = null)
    {
        return new DeliveryOrder(transaction, deliveryAddress, scheduledDate, notes);
    }
    
    public void MarkAsDelivered()
    {
        if (Status != DeliveryStatus.InTransit)
            throw new InvalidOperationException("Only in-transit orders can be marked as delivered");
            
        Status = DeliveryStatus.Delivered;
        DeliveredDate = DateTime.UtcNow;
    }
    
    public void UpdateStatus(DeliveryStatus newStatus)
    {
        if (Status == DeliveryStatus.Delivered || Status == DeliveryStatus.Cancelled)
            throw new InvalidOperationException("Cannot update status of delivered or cancelled orders");
            
        Status = newStatus;
    }
}