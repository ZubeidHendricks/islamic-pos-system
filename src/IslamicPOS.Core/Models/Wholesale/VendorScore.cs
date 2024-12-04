namespace IslamicPOS.Core.Models.Wholesale;

public class VendorScore
{
    public int VendorId { get; set; }
    public decimal QualityScore { get; set; }  // 0-100
    public decimal DeliveryScore { get; set; } // 0-100
    public decimal PriceCompetitiveness { get; set; } // 0-100
    public int TotalOrders { get; set; }
    public int ReturnedOrders { get; set; }
    public int LateDeliveries { get; set; }
    public int QualityComplaints { get; set; }
    public DateTime LastUpdated { get; set; }
    
    // Calculated Properties
    public decimal ReturnRate => TotalOrders > 0 ? (decimal)ReturnedOrders / TotalOrders * 100 : 0;
    public decimal LateDeliveryRate => TotalOrders > 0 ? (decimal)LateDeliveries / TotalOrders * 100 : 0;
    public decimal ComplaintRate => TotalOrders > 0 ? (decimal)QualityComplaints / TotalOrders * 100 : 0;
    
    // Overall reputation score (weighted average)
    public decimal ReputationScore =>
        (QualityScore * 0.4m) +
        (DeliveryScore * 0.3m) +
        (PriceCompetitiveness * 0.3m);
    
    // Auto-approval threshold
    public bool QualifiesForAutoApproval =>
        ReputationScore >= 85 &&
        ReturnRate < 5 &&
        LateDeliveryRate < 10 &&
        ComplaintRate < 5;
}