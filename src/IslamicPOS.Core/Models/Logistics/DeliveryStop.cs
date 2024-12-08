namespace IslamicPOS.Core.Models.Logistics;

public class DeliveryStop
{
    public int Id { get; set; }
    public int RouteId { get; set; }
    public int StopNumber { get; set; }
    public Guid OrderId { get; set; }
    public string DeliveryAddress { get; set; } = string.Empty;
    public double Latitude { get; set; }
    public double Longitude { get; set; }
    public DateTime EstimatedArrivalTime { get; set; }
    public DateTime? ActualArrivalTime { get; set; }
    public decimal LoadWeightKg { get; set; }
    public decimal LoadVolumeM3 { get; set; }
    public string Status { get; set; } = string.Empty;
    public string RecipientName { get; set; } = string.Empty;
    public string RecipientPhone { get; set; } = string.Empty;
    public string DeliveryNotes { get; set; } = string.Empty;
    public List<string> SpecialInstructions { get; set; } = new();
    public bool RequiresSignature { get; set; }
    public string? SignatureImage { get; set; }
    public string? ProofOfDeliveryImage { get; set; }
    
    // Navigation property
    public DeliveryRoute Route { get; set; } = null!;
}