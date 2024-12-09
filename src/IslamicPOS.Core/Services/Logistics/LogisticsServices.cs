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

    public interface IVendorDeliveryService
    {
        Task<List<TimeWindow>> GetAvailableTimeSlots(DateTime date);
        Task<bool> ReserveTimeSlot(DateTime date, TimeWindow slot);
        Task<List<DeliveryPoint>> GetActiveDeliveryPoints();
    }

    public interface IFleetManagementService
    {
        Task<MaintenanceSchedule> GetMaintenanceSchedule(string vehicleId);
        Task<List<VehicleAlert>> GetActiveAlerts();
        Task<bool> ScheduleMaintenance(string vehicleId, DateTime date);
    }

    public interface IDeliveryTrackingService
    {
        Task<DeliveryAlert> GetDeliveryAlert(int alertId);
        Task<List<DeliveryAlert>> GetActiveAlerts();
        Task<bool> MarkAlertAsResolved(int alertId);
    }

    public interface IDriverManagementService
    {
        Task<DriverSchedule> GetDriverSchedule(string driverId, DateTime date);
        Task<DriverPerformance> GetDriverPerformance(string driverId);
        Task<List<DriverAlert>> GetDriverAlerts(string driverId);
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