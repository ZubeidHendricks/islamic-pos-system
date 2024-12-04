using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using IslamicPOS.Core.Logistics.Models;

namespace IslamicPOS.Core.Logistics.Interfaces
{
    public interface IRouteOptimizationService
    {
        Task<OptimizedRoute> GenerateOptimalRoute(IEnumerable<DeliveryPoint> deliveryPoints);
        Task<TimeWindow> CalculateDeliveryWindow(DeliveryPoint point, VehicleType vehicle);
        Task<IEnumerable<OptimizedRoute>> GenerateDailyRoutes(DateTime date);
        Task<bool> ValidateRoute(OptimizedRoute route);
        Task<OptimizedRoute> UpdateRoute(Guid routeId, RouteStatus status);
    }
}