using IslamicPOS.Core.Models.Common;
using IslamicPOS.Core.Models.Transactions;

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