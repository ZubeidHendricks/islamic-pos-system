namespace IslamicPOS.Core.Models.Delivery
{
    public class DeliverySchedule
    {
        public int Id { get; set; }
        public DateTime ScheduleDate { get; set; }
        public int VehicleId { get; set; }
        public Vehicle Vehicle { get; set; }
        public string DriverId { get; set; }
        public ApplicationUser Driver { get; set; }
        public List<DeliveryOrder> Orders { get; set; }
        public string Status { get; set; }
        public string Notes { get; set; }
    }
}