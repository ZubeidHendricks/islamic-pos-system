namespace IslamicPOS.Infrastructure.Services.Delivery;

public class RouteOptimizationService : IRouteOptimizationService
{
    private readonly ILogger<RouteOptimizationService> _logger;

    public RouteOptimizationService(ILogger<RouteOptimizationService> logger)
    {
        _logger = logger;
    }

    public async Task<OptimizedRoute> GenerateRouteAsync(List<DeliveryPoint> points, Vehicle vehicle)
    {
        try
        {
            var route = new OptimizedRoute
            {
                VehicleId = vehicle.Id,
                Vehicle = vehicle,
                Date = DateTime.UtcNow.Date,
                Status = "Planned",
                IsHalalCertified = vehicle.IsHalalCertified
            };

            // Implement Clarke-Wright algorithm for route optimization
            route.Stops = OptimizeRouteUsingClarkeWright(points);
            route.TotalDistance = CalculateTotalDistance(route.Stops);
            route.EstimatedDuration = CalculateEstimatedDuration(route.Stops);

            return route;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error generating optimized route");
            throw;
        }
    }

    private List<DeliveryPoint> OptimizeRouteUsingClarkeWright(List<DeliveryPoint> points)
    {
        // Implementation of Clarke-Wright savings algorithm
        // This is a placeholder - actual implementation would be more complex
        return points.OrderBy(p => p.DeliveryWindow.Start).ToList();
    }

    private double CalculateTotalDistance(List<DeliveryPoint> stops)
    {
        // Calculate total distance using coordinates
        double totalDistance = 0;
        for (int i = 0; i < stops.Count - 1; i++)
        {
            totalDistance += CalculateDistance(
                stops[i].Latitude,
                stops[i].Longitude,
                stops[i + 1].Latitude,
                stops[i + 1].Longitude
            );
        }
        return totalDistance;
    }

    private TimeSpan CalculateEstimatedDuration(List<DeliveryPoint> stops)
    {
        // Estimate duration based on distance and average speed
        const double averageSpeedKmH = 40;
        var totalMinutes = (CalculateTotalDistance(stops) / averageSpeedKmH) * 60;
        // Add 15 minutes per stop for delivery time
        totalMinutes += stops.Count * 15;
        return TimeSpan.FromMinutes(totalMinutes);
    }

    private double CalculateDistance(double lat1, double lon1, double lat2, double lon2)
    {
        const double R = 6371; // Earth's radius in kilometers

        var dLat = ToRad(lat2 - lat1);
        var dLon = ToRad(lon2 - lon1);

        var a = Math.Sin(dLat / 2) * Math.Sin(dLat / 2) +
                Math.Cos(ToRad(lat1)) * Math.Cos(ToRad(lat2)) *
                Math.Sin(dLon / 2) * Math.Sin(dLon / 2);

        var c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));
        return R * c;
    }

    private double ToRad(double degrees) => degrees * Math.PI / 180;
}