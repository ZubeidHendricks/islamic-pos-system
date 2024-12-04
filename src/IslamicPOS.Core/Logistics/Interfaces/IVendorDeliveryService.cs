using System;
using System.Threading.Tasks;
using IslamicPOS.Core.Logistics.Models;

namespace IslamicPOS.Core.Logistics.Interfaces
{
    public interface IVendorDeliveryService
    {
        Task<TimeWindow> RequestDeliveryWindow(VendorDeliveryRequest request);
        Task<bool> ConfirmDeliverySchedule(Guid deliveryId);
        Task UpdateDeliveryStatus(Guid deliveryId, DeliveryStatus status);
        Task<DeliveryPoint> GetDeliveryDetails(Guid deliveryId);
    }

    public class VendorDeliveryRequest
    {
        public Guid VendorId { get; set; }
        public Guid OrderId { get; set; }
        public DateTime PreferredDate { get; set; }
        public TimeSpan PreferredTimeSlot { get; set; }
        public bool RequiresHalalCertification { get; set; }
    }

    public enum DeliveryStatus
    {
        Scheduled,
        InTransit,
        Delivered,
        Failed,
        Rescheduled
    }
}