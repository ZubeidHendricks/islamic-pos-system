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
}