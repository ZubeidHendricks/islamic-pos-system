namespace IslamicPOS.Core.Models.Delivery;

public class OptimizedRoute
{
    public int Id { get; set; }
    public int VehicleId { get; set; }
    public Vehicle Vehicle { get; set; }
    public DateTime Date { get; set; }
    public List<DeliveryPoint> Stops { get; set; } = new();
    public string Status { get; set; }
    public double TotalDistance { get; set; }
    public TimeSpan EstimatedDuration { get; set; }
    public bool IsHalalCertified { get; set; }
}