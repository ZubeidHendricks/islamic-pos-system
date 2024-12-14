using IslamicPOS.Core.Models.Base;
using IslamicPOS.Core.Models.Transaction;

namespace IslamicPOS.Core.Models.Logistics
{
    public class DeliveryOrder : EntityBase
    {
        public string OrderNumber { get; set; } = string.Empty;
        public int TransactionId { get; set; }
        public Transaction.Transaction Transaction { get; set; }
        public int DeliveryPointId { get; set; }
        public DeliveryPoint DeliveryPoint { get; set; }
        public DateTime RequestedDeliveryDate { get; set; }
        public DeliveryStatus Status { get; set; }
        public string? Notes { get; set; }
    }

    public enum DeliveryStatus
    {
        Pending,
        Assigned,
        InTransit,
        Delivered,
        Failed,
        Cancelled
    }
}