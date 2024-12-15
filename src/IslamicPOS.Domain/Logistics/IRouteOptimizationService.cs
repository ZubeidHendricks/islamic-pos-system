namespace IslamicPOS.Domain.Logistics;

public interface IRouteOptimizationService
{
    OptimizedRoute OptimizeRoute(List<string> destinations, Vehicle vehicle);
}