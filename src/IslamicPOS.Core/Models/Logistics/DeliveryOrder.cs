namespace IslamicPOS.Core.Models.Logistics
{
    public class DeliveryOrder
    {
        public int Id { get; set; }
        public string OrderNumber { get; set; } = string.Empty;
        public DeliveryPoint DeliveryPoint { get; set; } = new();
        public DateTime RequestedDate { get; set; }
        public TimeWindow RequestedTimeWindow { get; set; } = new();
        public DeliveryStatus Status { get; set; }
        public string Notes { get; set; } = string.Empty;
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