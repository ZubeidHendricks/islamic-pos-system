namespace IslamicPOS.Core.Models;

public class StockAlert
{
    public Guid ProductId { get; set; }
    public string ProductName { get; set; } = string.Empty;
    public string Category { get; set; } = string.Empty;
    public int CurrentStock { get; set; }
    public int MinimumStock { get; set; }
    public int ReorderPoint { get; set; }
    public DateTime LastRestocked { get; set; }
    public int AverageDailySales { get; set; }
    public bool IsOutOfStock => CurrentStock <= 0;
    public bool IsLowStock => CurrentStock <= ReorderPoint;
}