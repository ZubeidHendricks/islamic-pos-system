namespace IslamicPOS.Domain.Common.Models;

public class SaleSummary
{
    public decimal TotalRevenue { get; set; }
    public int TotalItemsSold { get; set; }
    public int UniqueProductsSold { get; set; }
}