namespace IslamicPOS.Core.Models.Logistics
{
    public class DeliveryAlert
    {
        public int Id { get; set; }
        public string AlertType { get; set; } = string.Empty;
        public string Message { get; set; } = string.Empty;
        public DateTime Timestamp { get; set; }
        public bool IsResolved { get; set; }
    }

    public class DriverSchedule
    {
        public int Id { get; set; }
        public string DriverId { get; set; } = string.Empty;
        public DateTime Date { get; set; }
        public List<DeliveryOrder> Deliveries { get; set; } = new();
    }

    public class DriverPerformance
    {
        public string DriverId { get; set; } = string.Empty;
        public int CompletedDeliveries { get; set; }
        public int DelayedDeliveries { get; set; }
        public double AverageDeliveryTime { get; set; }
        public decimal CustomerRating { get; set; }
    }

    public class MaintenanceSchedule
    {
        public int Id { get; set; }
        public string VehicleId { get; set; } = string.Empty;
        public DateTime ScheduledDate { get; set; }
        public string MaintenanceType { get; set; } = string.Empty;
        public string Notes { get; set; } = string.Empty;
    }

    public class VehicleAlert
    {
        public int Id { get; set; }
        public string VehicleId { get; set; } = string.Empty;
        public string AlertType { get; set; } = string.Empty;
        public string Message { get; set; } = string.Empty;
        public DateTime Timestamp { get; set; }
    }
}