namespace IslamicPOS.Core.Models.Delivery;

public class DeliverySchedule
{
    public int Id { get; set; }
    public DateTime ScheduleDate { get; set; }
    public string DriverId { get; set; } = string.Empty;
    public ApplicationUser? Driver { get; set; }
    public List<DeliveryOrder> Orders { get; set; } = new();
    public string Status { get; set; } = string.Empty;
    public string? Notes { get; set; }
}