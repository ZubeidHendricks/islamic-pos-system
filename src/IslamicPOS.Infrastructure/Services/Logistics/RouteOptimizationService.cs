using IslamicPOS.Core.Models.Logistics;
using IslamicPOS.Core.Services.Logistics;
using Microsoft.EntityFrameworkCore;

namespace IslamicPOS.Infrastructure.Services.Logistics;

public class RouteOptimizationService : IRouteOptimizationService
{
    private readonly ApplicationDbContext _context;
    private readonly ILogger<RouteOptimizationService> _logger;

    public RouteOptimizationService(ApplicationDbContext context, ILogger<RouteOptimizationService> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<List<DeliveryRoute>> GenerateOptimizedRoutes(List<DeliveryOrder> orders)
    {
        try
        {
            var routes = new List<DeliveryRoute>();
            var requirements = AnalyzeDeliveryRequirements(orders);

            // Get available resources
            var availableVehicles = await GetAvailableVehicles(requirements);
            var availableDrivers = await GetAvailableDrivers(requirements);

            if (!availableVehicles.Any() || !availableDrivers.Any())
            {
                throw new InvalidOperationException("Insufficient resources for delivery");
            }

            // Group orders by delivery zone and requirements
            var orderGroups = GroupOrdersByZone(orders);

            foreach (var group in orderGroups)
            {
                // Find best vehicle for this group
                var vehicle = FindOptimalVehicle(group.Value, availableVehicles);
                if (vehicle == null) continue;

                // Find best driver for this route
                var driver = FindOptimalDriver(group.Value, availableDrivers);
                if (driver == null) continue;

                // Create and optimize route
                var route = await CreateRoute(group.Value, vehicle, driver);
                route = await OptimizeRoute(route);

                if (await ValidateRoute(route))
                {
                    routes.Add(route);
                    // Update resource availability
                    availableVehicles.Remove(vehicle);
                    availableDrivers.Remove(driver);
                }
            }

            return routes;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error generating optimized routes");
            throw;
        }
    }

    public async Task<DeliveryRoute> OptimizeRoute(DeliveryRoute route)
    {
        // Implement TSP (Traveling Salesman Problem) optimization
        var stops = route.Stops.ToList();
        var optimizedStops = new List<DeliveryStop>();
        var currentStop = stops.First();
        stops.RemoveAt(0);

        while (stops.Any())
        {
            optimizedStops.Add(currentStop);
            if (!stops.Any()) break;

            // Find nearest next stop
            var nearest = FindNearestStop(currentStop, stops);
            stops.Remove(nearest);
            currentStop = nearest;
        }

        optimizedStops.Add(currentStop);

        // Update stop sequence and timing
        for (int i = 0; i < optimizedStops.Count; i++)
        {
            optimizedStops[i].StopNumber = i + 1;
            optimizedStops[i].EstimatedArrivalTime = CalculateArrivalTime(
                route.StartTime,
                optimizedStops.Take(i + 1).ToList()
            );
        }

        route.Stops = optimizedStops;
        route.TotalDistanceKm = CalculateTotalDistance(optimizedStops);
        route.EstimatedEndTime = optimizedStops.Last().EstimatedArrivalTime.AddMinutes(30);

        return route;
    }

    public async Task<bool> ValidateRoute(DeliveryRoute route)
    {
        // Validate vehicle capacity
        if (route.TotalLoadKg > route.Vehicle.LoadCapacityKg ||
            route.TotalVolumeM3 > route.Vehicle.VolumeCapacityM3)
        {
            return false;
        }

        // Validate delivery windows
        foreach (var stop in route.Stops)
        {
            if (stop.EstimatedArrivalTime > stop.RequiredDeliveryTime)
            {
                return false;
            }
        }

        // Validate driver hours
        var driverSchedule = await GetDriverSchedule(route.DriverId);
        if (!IsWithinDriverHours(route, driverSchedule))
        {
            return false;
        }

        // Validate halal requirements
        if (route.RequiresHalalCertification)
        {
            if (!route.Vehicle.IsHalalCertified || !route.Driver.HasHalalCertification)
            {
                return false;
            }
        }

        return true;
    }

    private DeliveryRequirements AnalyzeDeliveryRequirements(List<DeliveryOrder> orders)
    {
        return new DeliveryRequirements
        {
            TotalWeightKg = orders.Sum(o => o.TotalWeightKg),
            TotalVolumeM3 = orders.Sum(o => o.TotalVolumeM3),
            RequiresRefrigeration = orders.Any(o => o.RequiresRefrigeration),
            RequiredTemperature = orders
                .Where(o => o.RequiredTemperature.HasValue)
                .Min(o => o.RequiredTemperature),
            RequiresHalalCertification = orders.Any(o => o.RequiresHalalCertification),
            SpecialRequirements = orders
                .SelectMany(o => o.SpecialRequirements)
                .Distinct()
                .ToList(),
            DeliveryZones = orders
                .Select(o => o.DeliveryZone)
                .Distinct()
                .ToList()
        };
    }

    private Dictionary<string, List<DeliveryOrder>> GroupOrdersByZone(List<DeliveryOrder> orders)
    {
        return orders.GroupBy(o => o.DeliveryZone)
                     .ToDictionary(g => g.Key, g => g.ToList());
    }

    private DeliveryVehicle FindOptimalVehicle(List<DeliveryOrder> orders, List<DeliveryVehicle> vehicles)
    {
        var requirements = AnalyzeDeliveryRequirements(orders);

        return vehicles
            .Where(v => v.LoadCapacityKg >= requirements.TotalWeightKg &&
                       v.VolumeCapacityM3 >= requirements.TotalVolumeM3 &&
                       (!requirements.RequiresRefrigeration || v.HasRefrigeration) &&
                       (!requirements.RequiresHalalCertification || v.IsHalalCertified))
            .OrderBy(v => v.LoadCapacityKg) // Choose smallest suitable vehicle
            .FirstOrDefault();
    }

    private Driver FindOptimalDriver(List<DeliveryOrder> orders, List<Driver> drivers)
    {
        var requirements = AnalyzeDeliveryRequirements(orders);

        return drivers
            .Where(d => d.IsAvailable &&
                       (!requirements.RequiresHalalCertification || d.HasHalalCertification))
            .OrderByDescending(d => d.SuccessRate)
            .FirstOrDefault();
    }

    private DeliveryStop FindNearestStop(DeliveryStop current, List<DeliveryStop> stops)
    {
        return stops.OrderBy(s => CalculateDistance(
            current.Latitude, current.Longitude,
            s.Latitude, s.Longitude
        )).First();
    }

    private double CalculateDistance(double lat1, double lon1, double lat2, double lon2)
    {
        // Haversine formula for distance calculation
        const double R = 6371; // Earth's radius in km

        var dLat = ToRad(lat2 - lat1);
        var dLon = ToRad(lon2 - lon1);

        var a = Math.Sin(dLat / 2) * Math.Sin(dLat / 2) +
                Math.Cos(ToRad(lat1)) * Math.Cos(ToRad(lat2)) *
                Math.Sin(dLon / 2) * Math.Sin(dLon / 2);

        var c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));
        return R * c;
    }

    private double ToRad(double deg)
    {
        return deg * (Math.PI / 180);
    }

    private DateTime CalculateArrivalTime(DateTime startTime, List<DeliveryStop> stops)
    {
        var time = startTime;
        for (int i = 1; i < stops.Count; i++)
        {
            var distance = CalculateDistance(
                stops[i - 1].Latitude, stops[i - 1].Longitude,
                stops[i].Latitude, stops[i].Longitude
            );

            // Assume average speed of 40 km/h in urban areas
            var travelTime = (distance / 40.0) * 60; // Minutes
            time = time.AddMinutes(travelTime + 15); // Add 15 min for delivery
        }
        return time;
    }

    private decimal CalculateTotalDistance(List<DeliveryStop> stops)
    {
        decimal total = 0;
        for (int i = 1; i < stops.Count; i++)
        {
            total += (decimal)CalculateDistance(
                stops[i - 1].Latitude, stops[i - 1].Longitude,
                stops[i].Latitude, stops[i].Longitude
            );
        }
        return total;
    }

    private async Task<bool> IsWithinDriverHours(DeliveryRoute route, DriverSchedule schedule)
    {
        // Check if route fits within driver's allowed working hours
        var routeDuration = (route.EstimatedEndTime - route.StartTime).TotalHours;
        var totalHours = schedule.WorkedHours + routeDuration;

        return totalHours <= 8; // 8-hour shift limit
    }
}