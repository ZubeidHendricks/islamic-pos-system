using IslamicPOS.Core.Models.Logistics;

namespace IslamicPOS.Core.Services.Logistics;

public interface IDeliveryTrackingService
{
    Task<DeliveryStatus> GetDeliveryStatus(int routeId);
    Task<DeliveryStatus> GetStopStatus(int stopId);
    Task<bool> UpdateDeliveryStatus(int routeId, string status);
    Task<bool> UpdateStopStatus(int stopId, string status);
    Task<bool> RecordDelivery(int stopId, DeliveryProof proof);
    Task<bool> RecordIssue(int stopId, DeliveryIssue issue);
    Task<DeliveryRoute> GetActiveRoute(string driverId);
    Task<List<DeliveryStop>> GetPendingStops(int routeId);
    Task<List<DeliveryAlert>> GetDeliveryAlerts();
    Task<DeliveryMetrics> GetDeliveryMetrics(DateTime startDate, DateTime endDate);
}