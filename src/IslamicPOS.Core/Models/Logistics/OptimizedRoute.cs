namespace IslamicPOS.Core.Models.Logistics
{
    public class OptimizedRoute
    {
        public int Id { get; set; }
        public List<DeliveryPoint> Points { get; set; } = new();
        public decimal TotalDistance { get; set; }
        public TimeSpan EstimatedDuration { get; set; }
        public DateTime StartTime { get; set; }
    }
}