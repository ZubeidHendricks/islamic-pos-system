using IslamicPOS.Core.Models.Logistics;

namespace IslamicPOS.Core.Services.Logistics
{
    public interface IRouteOptimizationService
    {
        Task<OptimizedRoute> OptimizeDeliveryRoute(List<DeliveryPoint> deliveryPoints);
        Task<TimeWindow> GetOptimalDeliveryWindow(DeliveryPoint point);
        Task<List<OptimizedRoute>> GetDailyRoutes();
        Task<OptimizedRoute> UpdateRoute(int routeId);
    }

    public class OptimizedRoute
    {
        public int Id { get; set; }
        public List<DeliveryPoint> Points { get; set; } = new();
        public decimal TotalDistance { get; set; }
        public TimeSpan EstimatedDuration { get; set; }
        public DateTime StartTime { get; set; }
    }
}