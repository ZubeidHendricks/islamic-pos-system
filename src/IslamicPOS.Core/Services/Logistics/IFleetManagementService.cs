using IslamicPOS.Core.Models.Logistics;

namespace IslamicPOS.Core.Services.Logistics;

public interface IFleetManagementService
{
    Task<DeliveryVehicle> AddVehicle(DeliveryVehicle vehicle);
    Task<DeliveryVehicle> UpdateVehicle(DeliveryVehicle vehicle);
    Task<bool> DeleteVehicle(int id);
    Task<DeliveryVehicle> GetVehicle(int id);
    Task<List<DeliveryVehicle>> GetAllVehicles();
    Task<List<DeliveryVehicle>> GetAvailableVehicles();
    Task<bool> AssignVehicle(int vehicleId, int routeId);
    Task<bool> UpdateVehicleLocation(int vehicleId, double latitude, double longitude);
    Task<MaintenanceSchedule> GetMaintenanceSchedule(int vehicleId);
    Task<List<VehicleAlert>> GetVehicleAlerts();
    Task<bool> VerifyHalalCompliance(int vehicleId);
}