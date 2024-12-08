using IslamicPOS.Core.Models.Logistics;

namespace IslamicPOS.Core.Services.Delivery;

public interface IRouteOptimizationService
{
    Task<OptimizedRoute> OptimizeRouteAsync(List<DeliveryPoint> points, Vehicle vehicle);
    Task<OptimizedRoute> ReoptimizeRouteAsync(OptimizedRoute currentRoute, List<DeliveryOrder> newOrders);
    Task<DeliverySchedule> GenerateScheduleAsync(DateTime date, List<DeliveryOrder> orders);
}