namespace IslamicPOS.Core.Services.Delivery;

public interface IRouteOptimizationService
{
    Task<OptimizedRoute> GenerateRouteAsync(List<DeliveryPoint> points, Vehicle vehicle);
    Task<List<OptimizedRoute>> GenerateDailyRoutesAsync(DateTime date);
    Task<bool> ValidateHalalRequirementsAsync(OptimizedRoute route);
}