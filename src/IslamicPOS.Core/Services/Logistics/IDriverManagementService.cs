using IslamicPOS.Core.Models.Logistics;

namespace IslamicPOS.Core.Services.Logistics;

public interface IDriverManagementService
{
    Task<Driver> AddDriver(Driver driver);
    Task<Driver> UpdateDriver(Driver driver);
    Task<bool> DeleteDriver(string id);
    Task<Driver> GetDriver(string id);
    Task<List<Driver>> GetAllDrivers();
    Task<List<Driver>> GetAvailableDrivers();
    Task<bool> AssignDriver(string driverId, int routeId);
    Task<bool> UpdateDriverLocation(string driverId, double latitude, double longitude);
    Task<List<DriverSchedule>> GetDriverSchedules();
    Task<DriverPerformance> GetDriverPerformance(string driverId);
    Task<bool> VerifyDriverCertifications(string driverId);
    Task<List<DriverAlert>> GetDriverAlerts();
}