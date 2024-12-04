namespace IslamicPOS.Core.Models;

public class SupplierPerformance
{
    public int SupplierId { get; set; }
    public string SupplierName { get; set; } = string.Empty;
    public int TotalOrders { get; set; }
    public decimal TotalValue { get; set; }
    public int OnTimeDeliveries { get; set; }
    public int LateDeliveries { get; set; }
    public int ReturnedOrders { get; set; }
    public double OnTimeDeliveryRate => TotalOrders > 0 ? (OnTimeDeliveries / (double)TotalOrders) * 100 : 0;
    public decimal AverageOrderValue => TotalOrders > 0 ? TotalValue / TotalOrders : 0;
}