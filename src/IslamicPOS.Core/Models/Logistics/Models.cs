namespace IslamicPOS.Core.Models.Logistics
{
    public class DeliveryPoint
    {
        public new int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public decimal Latitude { get; set; }
        public decimal Longitude { get; set; }
        public TimeWindow DeliveryWindow { get; set; } = new();
    }

    public class TimeWindow
    {
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
        public DayOfWeek[] AvailableDays { get; set; } = Array.Empty<DayOfWeek>();
    }

    public class DeliveryOrder
    {
        public new int Id { get; set; }
        public string OrderNumber { get; set; } = string.Empty;
        public DeliveryPoint DeliveryPoint { get; set; } = new();
        public DateTime RequestedDate { get; set; }
        public TimeWindow RequestedTimeWindow { get; set; } = new();
        public DeliveryStatus Status { get; set; }
        public string Notes { get; set; } = string.Empty;
    }

    public enum DeliveryStatus
    {
        Pending,
        Assigned,
        InTransit,
        Delivered,
        Failed,
        Cancelled
    }

    public class Vehicle
    {
        public new int Id { get; set; }
        public string RegistrationNumber { get; set; } = string.Empty;
        public string Model { get; set; } = string.Empty;
        public decimal MaxCapacity { get; set; }
        public bool IsActive { get; set; }
    }

    public class DriverAlert
    {
        public int Id { get; set; }
        public string DriverId { get; set; } = string.Empty;
        public string AlertType { get; set; } = string.Empty;
        public string Message { get; set; } = string.Empty;
        public DateTime Timestamp { get; set; }
        public bool IsResolved { get; set; }
    }
}