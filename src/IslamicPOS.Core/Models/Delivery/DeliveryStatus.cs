namespace IslamicPOS.Core.Models.Delivery
{
    public class DeliveryStatus
    {
        public int Id { get; set; }
        public int DeliveryOrderId { get; set; }
        public DeliveryOrder DeliveryOrder { get; set; }
        public string Status { get; set; }
        public DateTime Timestamp { get; set; }
        public string UpdatedBy { get; set; }
        public string Notes { get; set; }
        public string Location { get; set; }
    }
}