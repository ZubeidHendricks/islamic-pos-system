using IslamicPOS.Core.Models.Common;

namespace IslamicPOS.Core.Models.Logistics;

public class DeliveryPoint : Entity
{
    public string Name { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
    public double Latitude { get; set; }
    public double Longitude { get; set; }
    public string ContactName { get; set; } = string.Empty;
    public string ContactPhone { get; set; } = string.Empty;
    public string? Instructions { get; set; }
}

public class Vehicle : Entity
{
    public string RegistrationNumber { get; set; } = string.Empty;
    public string Model { get; set; } = string.Empty;
    public string Make { get; set; } = string.Empty;
    public decimal LoadCapacity { get; set; }
    public string Status { get; set; } = string.Empty;
    public DateTime? LastMaintenanceDate { get; set; }
    public DateTime? NextMaintenanceDate { get; set; }
}

public class DeliveryOrder : Entity
{
    public Guid TransactionId { get; set; }
    public Transaction Transaction { get; set; } = null!;
    public Guid DeliveryPointId { get; set; }
    public DeliveryPoint DeliveryPoint { get; set; } = null!;
    public DateTime RequestedDate { get; set; }
    public TimeSpan DeliveryWindow { get; set; }
    public string Status { get; set; } = string.Empty;
    public Guid? VehicleId { get; set; }
    public Vehicle? Vehicle { get; set; }
}

public class DeliveryStatus : Entity
{
    public Guid DeliveryOrderId { get; set; }
    public DeliveryOrder DeliveryOrder { get; set; } = null!;
    public string Status { get; set; } = string.Empty;
    public string? Location { get; set; }
    public string? Notes { get; set; }
}

public class DeliverySchedule : Entity
{
    public DateTime ScheduleDate { get; set; }
    public Guid VehicleId { get; set; }
    public Vehicle Vehicle { get; set; } = null!;
    public List<DeliveryOrder> Orders { get; set; } = new();
    public string Status { get; set; } = string.Empty;
}

public class OptimizedRoute : Entity
{
    public Guid VehicleId { get; set; }
    public Vehicle Vehicle { get; set; } = null!;
    public List<DeliveryPoint> Points { get; set; } = new();
    public double TotalDistance { get; set; }
    public TimeSpan EstimatedDuration { get; set; }
    public List<DeliveryOrder> Orders { get; set; } = new();
}

public class DriverSchedule : Entity
{
    public string DriverId { get; set; } = string.Empty;
    public DateTime Date { get; set; }
    public List<DeliveryOrder> Orders { get; set; } = new();
    public string Status { get; set; } = string.Empty;
}

public class DriverPerformance : Entity
{
    public string DriverId { get; set; } = string.Empty;
    public DateTime Date { get; set; }
    public int DeliveriesCompleted { get; set; }
    public int DeliveriesFailed { get; set; }
    public TimeSpan AverageDeliveryTime { get; set; }
}

public class DriverAlert : Entity
{
    public string DriverId { get; set; } = string.Empty;
    public string AlertType { get; set; } = string.Empty;
    public string Message { get; set; } = string.Empty;
    public bool IsResolved { get; set; }
}