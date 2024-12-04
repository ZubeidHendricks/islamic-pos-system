using IslamicPOS.Core.Models.Logistics;

namespace IslamicPOS.Core.Services.Logistics;

public interface IRouteOptimizationService
{
    Task<List<DeliveryRoute>> GenerateOptimizedRoutes(List<DeliveryOrder> orders);
    Task<DeliveryRoute> OptimizeRoute(DeliveryRoute route);
    Task<bool> ValidateRoute(DeliveryRoute route);
    Task<List<DeliveryVehicle>> GetAvailableVehicles(DeliveryRequirements requirements);
    Task<List<Driver>> GetAvailableDrivers(DeliveryRequirements requirements);
    Task<DeliverySchedule> GenerateSchedule(List<DeliveryRoute> routes);
    Task<DeliveryMetrics> CalculateRouteMetrics(DeliveryRoute route);
    Task<List<DeliveryRoute>> ReoptimizeRoutes(List<DeliveryRoute> routes);
}