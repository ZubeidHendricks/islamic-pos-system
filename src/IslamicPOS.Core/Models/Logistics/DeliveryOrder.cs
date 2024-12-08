using IslamicPOS.Core.Models.Common;
using IslamicPOS.Core.Models.Transaction;

namespace IslamicPOS.Core.Models.Logistics;

public class DeliveryOrder : Entity
{
    public Guid TransactionId { get; set; }
    public Transaction.Transaction Transaction { get; set; } = null!;
    public Guid DeliveryPointId { get; set; }
    public DeliveryPoint DeliveryPoint { get; set; } = null!;
    public DateTime RequestedDate { get; set; }
    public TimeSpan DeliveryWindow { get; set; }
    public string Status { get; set; } = string.Empty;
    public Guid? VehicleId { get; set; }
    public Vehicle? Vehicle { get; set; }
    public List<DeliveryStatus> StatusUpdates { get; set; } = new();
    public List<DeliveryAlert> Alerts { get; set; } = new();
}

public class DeliveryStatus : Entity
{
    public Guid DeliveryOrderId { get; set; }
    public DeliveryOrder DeliveryOrder { get; set; } = null!;
    public string Status { get; set; } = string.Empty;
    public string? Location { get; set; }
    public string? Notes { get; set; }
}

public class DeliveryAlert : Entity
{
    public Guid DeliveryOrderId { get; set; }
    public DeliveryOrder DeliveryOrder { get; set; } = null!;
    public string AlertType { get; set; } = string.Empty;
    public string Message { get; set; } = string.Empty;
    public bool IsResolved { get; set; }
    public DateTime? ResolvedAt { get; set; }
}