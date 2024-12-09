using IslamicPOS.Core.Models.Logistics;

namespace IslamicPOS.Core.Services.Logistics
{
    public interface IVendorDeliveryService
    {
        Task<List<TimeWindow>> GetAvailableTimeSlots(DateTime date);
        Task<bool> ReserveTimeSlot(DateTime date, TimeWindow slot);
        Task<List<DeliveryPoint>> GetActiveDeliveryPoints();
    }
}