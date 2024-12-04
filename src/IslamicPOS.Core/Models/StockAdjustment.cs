namespace IslamicPOS.Core.Models;

public class StockAdjustment
{
    public Guid Id { get; set; }
    public Guid ProductId { get; set; }
    public string ProductName { get; set; } = string.Empty;
    public int QuantityBefore { get; set; }
    public int QuantityAfter { get; set; }
    public int Adjustment => QuantityAfter - QuantityBefore;
    public string Reason { get; set; } = string.Empty;
    public string AdjustedBy { get; set; } = string.Empty;
    public DateTime Timestamp { get; set; }
}