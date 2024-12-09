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
}