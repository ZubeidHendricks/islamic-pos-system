namespace IslamicPOS.Core.Models.Delivery
{
    public class OptimizedRoute
    {
        public int Id { get; set; }
        public int DeliveryScheduleId { get; set; }
        public DeliverySchedule Schedule { get; set; }
        public List<DeliveryPoint> RoutePoints { get; set; }
        public double TotalDistance { get; set; }
        public TimeSpan EstimatedDuration { get; set; }
        public string RoutePolyline { get; set; }
    }
}