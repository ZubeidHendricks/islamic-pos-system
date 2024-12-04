using IslamicPOS.Core.Models.Logistics;
using IslamicPOS.Core.Services.Logistics;
using Microsoft.EntityFrameworkCore;

namespace IslamicPOS.Infrastructure.Services.Logistics;

public class FleetManagementService : IFleetManagementService
{
    private readonly ApplicationDbContext _context;
    private readonly ILogger<FleetManagementService> _logger;
    private readonly IHalalComplianceService _halalService;

    public FleetManagementService(
        ApplicationDbContext context,
        ILogger<FleetManagementService> logger,
        IHalalComplianceService halalService)
    {
        _context = context;
        _logger = logger;
        _halalService = halalService;
    }

    public async Task<List<DeliveryVehicle>> GetAvailableVehicles()
    {
        return await _context.Vehicles
            .Where(v => v.Status == "Available" && 
                       v.NextMaintenanceDate > DateTime.UtcNow)
            .OrderBy(v => v.LastMaintenanceDate)
            .ToListAsync();
    }

    public async Task<bool> AssignVehicle(int vehicleId, int routeId)
    {
        var vehicle = await _context.Vehicles.FindAsync(vehicleId)
            ?? throw new KeyNotFoundException($"Vehicle {vehicleId} not found");

        var route = await _context.DeliveryRoutes.FindAsync(routeId)
            ?? throw new KeyNotFoundException($"Route {routeId} not found");

        // Validate vehicle availability
        if (vehicle.Status != "Available")
            throw new InvalidOperationException("Vehicle is not available");

        // Check vehicle capabilities against route requirements
        if (!CanHandleRoute(vehicle, route))
            throw new InvalidOperationException("Vehicle does not meet route requirements");

        // Assign vehicle
        route.VehicleId = vehicleId;
        vehicle.Status = "Assigned";
        await _context.SaveChangesAsync();

        return true;
    }

    public async Task<bool> UpdateVehicleLocation(int vehicleId, double latitude, double longitude)
    {
        var vehicle = await _context.Vehicles.FindAsync(vehicleId)
            ?? throw new KeyNotFoundException($"Vehicle {vehicleId} not found");

        vehicle.CurrentLatitude = latitude;
        vehicle.CurrentLongitude = longitude;
        vehicle.CurrentLocation = await GetLocationAddress(latitude, longitude);

        // Update associated active route if exists
        var activeRoute = await _context.DeliveryRoutes
            .FirstOrDefaultAsync(r => r.VehicleId == vehicleId && r.Status == "InProgress");

        if (activeRoute != null)
        {
            await UpdateRouteProgress(activeRoute, latitude, longitude);
        }

        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<MaintenanceSchedule> GetMaintenanceSchedule(int vehicleId)
    {
        var vehicle = await _context.Vehicles.FindAsync(vehicleId)
            ?? throw new KeyNotFoundException($"Vehicle {vehicleId} not found");

        var schedule = new MaintenanceSchedule
        {
            VehicleId = vehicleId,
            LastMaintenanceDate = vehicle.LastMaintenanceDate,
            NextMaintenanceDate = vehicle.NextMaintenanceDate,
            MaintenanceItems = await GenerateMaintenanceItems(vehicle)
        };

        // Add upcoming maintenance alerts
        var daysUntilMaintenance = (vehicle.NextMaintenanceDate - DateTime.UtcNow).TotalDays;
        if (daysUntilMaintenance <= 7)
        {
            schedule.Alerts.Add(new MaintenanceAlert
            {
                Type = "Scheduled Maintenance Due",
                Description = $"Maintenance due in {Math.Ceiling(daysUntilMaintenance)} days",
                Priority = daysUntilMaintenance <= 2 ? "High" : "Medium"
            });
        }

        return schedule;
    }

    public async Task<List<VehicleAlert>> GetVehicleAlerts()
    {
        var alerts = new List<VehicleAlert>();

        // Get all active vehicles
        var vehicles = await _context.Vehicles
            .Where(v => v.Status != "Retired")
            .ToListAsync();

        foreach (var vehicle in vehicles)
        {
            // Check maintenance due
            if (vehicle.NextMaintenanceDate <= DateTime.UtcNow.AddDays(7))
            {
                alerts.Add(new VehicleAlert
                {
                    VehicleId = vehicle.Id,
                    Type = "Maintenance",
                    Priority = "High",
                    Message = $"Maintenance due on {vehicle.NextMaintenanceDate:MMM dd, yyyy}"
                });
            }

            // Check Halal certification expiry
            if (vehicle.IsHalalCertified && 
                vehicle.HalalCertificationExpiry <= DateTime.UtcNow.AddMonths(1))
            {
                alerts.Add(new VehicleAlert
                {
                    VehicleId = vehicle.Id,
                    Type = "Certification",
                    Priority = "High",
                    Message = "Halal certification expiring soon"
                });
            }

            // Check high mileage
            if (vehicle.TotalDistanceCovered >= 50000) // 50,000 km threshold
            {
                alerts.Add(new VehicleAlert
                {
                    VehicleId = vehicle.Id,
                    Type = "Mileage",
                    Priority = "Medium",
                    Message = "High mileage - consider maintenance check"
                });
            }
        }

        return alerts;
    }

    public async Task<bool> VerifyHalalCompliance(int vehicleId)
    {
        var vehicle = await _context.Vehicles.FindAsync(vehicleId)
            ?? throw new KeyNotFoundException($"Vehicle {vehicleId} not found");

        if (!vehicle.IsHalalCertified)
            return false;

        // Verify certification is valid
        if (!vehicle.HalalCertificationExpiry.HasValue || 
            vehicle.HalalCertificationExpiry <= DateTime.UtcNow)
            return false;

        // Verify certification with authority
        var isValid = await _halalService.VerifyCertification(
            vehicle.HalalCertificationNumber,
            "VEHICLE");

        if (!isValid)
        {
            vehicle.IsHalalCertified = false;
            await _context.SaveChangesAsync();
            return false;
        }

        return true;
    }

    private async Task<List<MaintenanceItem>> GenerateMaintenanceItems(DeliveryVehicle vehicle)
    {
        var items = new List<MaintenanceItem>();

        // Regular maintenance items
        items.Add(new MaintenanceItem
        {
            Type = "Oil Change",
            DueDate = vehicle.LastMaintenanceDate.AddMonths(3),
            Status = "Pending",
            Priority = "High"
        });

        items.Add(new MaintenanceItem
        {
            Type = "Tire Rotation",
            DueDate = vehicle.LastMaintenanceDate.AddMonths(6),
            Status = "Pending",
            Priority = "Medium"
        });

        // Special items for refrigerated vehicles
        if (vehicle.HasRefrigeration)
        {
            items.Add(new MaintenanceItem
            {
                Type = "Refrigeration System Check",
                DueDate = vehicle.LastMaintenanceDate.AddMonths(1),
                Status = "Pending",
                Priority = "High"
            });
        }

        return items;
    }

    private bool CanHandleRoute(DeliveryVehicle vehicle, DeliveryRoute route)
    {
        return vehicle.LoadCapacityKg >= route.TotalLoadKg &&
               vehicle.VolumeCapacityM3 >= route.TotalVolumeM3 &&
               (!route.RequiresRefrigeration || vehicle.HasRefrigeration) &&
               (!route.RequiresHalalCertification || vehicle.IsHalalCertified);
    }

    private async Task UpdateRouteProgress(DeliveryRoute route, double latitude, double longitude)
    {
        // Find next pending stop
        var nextStop = route.Stops
            .FirstOrDefault(s => s.Status == "Pending");

        if (nextStop != null)
        {
            // Calculate distance to next stop
            var distance = CalculateDistance(latitude, longitude, 
                nextStop.Latitude, nextStop.Longitude);

            // Update ETA if significant change
            if (Math.Abs(distance - route.CurrentDistanceToNextStop) > 0.5) // 500m threshold
            {
                route.CurrentDistanceToNextStop = distance;
                route.EstimatedArrivalToNextStop = CalculateETA(distance);
            }
        }
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

    private double ToRad(double deg)
    {
        return deg * (Math.PI / 180);
    }

    private DateTime CalculateETA(double distance)
    {
        // Assume average speed of 40 km/h in urban areas
        var hours = distance / 40.0;
        return DateTime.UtcNow.AddHours(hours);
    }

    private async Task<string> GetLocationAddress(double latitude, double longitude)
    {
        // TODO: Implement reverse geocoding
        return $"{latitude:F6}, {longitude:F6}";
    }
}